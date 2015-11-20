using SistemaArmazem.Models.Entities;
using SistemaArmazem.Models.Negocio;
using SistemaArmazem.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArmazem.Controllers
{
    public class HomeController : Controller
    {
        ClienteRepository clienteRepository = new ClienteRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contato(string nome, string email, string texto)
        {
            try
            {
                EnviarMensagem em = new EnviarMensagem("daniel.hpassos@gmail.com", "daniel.hpassos@gmail.com", "Vindo do Sistema de Armazem", texto, nome);
                em.SubmeterEmail();
                TempData["certo"] = "Enviado com Sucesso!";
                return View();
            }
            catch (Exception ex)
            {
                TempData["certo"] = "Falha no envio!. Erro: " + ex.Message;
                return View();
            }
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(string nome, string razao, string titular, string login, string senha,
            string senha2, string email, string telefone, string celular, string cep, string rua,
            string bairro, string cidade, string estado, string pais, string numero, string grupo)
        {
            try
            {
                Cliente cliente = new Cliente()
                {
                    nome = nome,
                    razao = razao,
                    titular = titular,
                    login = login,
                    senha = senha,
                    email = email,
                    telefone = telefone,
                    celular = celular,
                    cep = cep,
                    rua = rua,
                    bairro = bairro,
                    cidade = cidade,
                    estado = estado,
                    pais = pais,
                    numero = numero,
                    grupo = grupo
                };


                clienteRepository.Add(cliente);

                EnviarMensagem em = new EnviarMensagem("daniel.hpassos@gmail.com", email, "Vindo do Sistema de Armazem", String.Format("Obrigado por se cadastrar em nosso sistema!\n\nSeus dados de acesso são,\nLogin: {0}\nSenha: {1}", login, senha), "Daniel");
                em.SubmeterEmail();

                TempData["certo"] = "Cadastrado com sucesso!. Um e-mail contendo seu login e senha foram enviados para o seu e-mail do cadastro.";
                return View();
            }
            catch (Exception ex)
            {
                TempData["certo"] = "Falha no cadastro. Erro: " + ex.Message;
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string senha)
        {
            try
            {
                var cliente = clienteRepository.Listar().FirstOrDefault(x => x.login == login && x.senha == senha);
                if (cliente != null)
                {
                    Session.Add("User", cliente);
                    TempData["certo"] = "Logado com sucesso!";
                    if (((Cliente)Session["User"]).grupo == "member")
                        return View("PaineldeControleUsuario");
                    else
                        return View("PaineldeControleAdmin");

                }
                else
                {
                    TempData["erro"] = "Email ou Senha invalido!";
                    return View();
                }
            }
            catch (Exception ex)
            {

                TempData["erro"] = "Falha no login. Erro: " + ex.Message;
                return View();
            }
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return View("Index");
        }

        public ActionResult Simulacao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Simulacao(string classe, string subclasse, string armazenagem, int idarmazem, int qtdarmazem,
            DateTime dtInicio, DateTime dtFim)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(classe) && string.IsNullOrWhiteSpace(subclasse) && string.IsNullOrWhiteSpace(subclasse) && string.IsNullOrWhiteSpace(armazenagem))
                {
                    TempData["erro"] = "Não deixe espaços em branco!";
                    return View();
                }
                if (DateTime.Now.Ticks > dtInicio.Ticks)
                {
                    TempData["erro"] = "A data de inicio não pode ser menor que a data de hoje!";
                    return View();
                }
                if (dtFim.Ticks <= dtInicio.Ticks)
                {
                    TempData["erro"] = "A data de Fim não pode ser menor que a data de Inicio!";
                    return View();
                }
                if (qtdarmazem <= 0)
                {
                    TempData["erro"] = "Quantidade de espaço não pode ser zero ou vazia!";
                    return View();
                }
                if (Negocio.armazemTemEspacoPraIsso(qtdarmazem, idarmazem))
                {
                    var dias = Negocio.retornaDiferencaEntreDuasDatas(dtFim, dtInicio);
                    decimal valorTotal = Convert.ToInt32(((dias * 10) * qtdarmazem) + 1000);

                    if (Session["User"] != null)
                    {
                        PedidoRepository pedidoRepository = new PedidoRepository();
                        ArmazemRepository armazemRepository = new ArmazemRepository();
                        Armazem armazem = new Armazem();

                        var nome = ((Cliente)(Session["User"])).nome;
                        var email = ((Cliente)(Session["User"])).email;
                        var clienteID = ((Cliente)(Session["User"])).clienteId;

                        armazem.clienteId = clienteID;
                        armazem.usadoArmazem = qtdarmazem;
                        armazem.tamanhoArmazemId = idarmazem;
                        //armazemRepository.Add(armazem);

                        Pedido pedido = new Pedido()
                        {
                            clienteId = clienteID,
                            classeId = Convert.ToInt32(classe),
                            subclasseId = Convert.ToInt32(subclasse),
                            armazenagemId = Convert.ToInt32(armazenagem),
                            armazem = armazem,//Convert.ToInt32(idarmazem),
                            dtInicio = dtInicio,
                            dtFim = dtFim,
                            valorTotal = valorTotal,
                            ckstatus = false
                        };

                        pedidoRepository.Add(pedido);

                        string msg = "Valor total a ser pago e: " + valorTotal + "\nOs dados do seu pedido, serao enviados para o seu e-mail.\nVerifique sua caixa de entrada.";
                        EnviarMensagem em = new EnviarMensagem("daniel.hpassos@gmail.com", email, "Vindo do Sistema de Armazem", String.Format("Olá {0},\n\nObrigado por realizar o pedido em nosso sistema!\n\nOs dados do seu pedido são,\nCliente: {0}\nClasse Prod: {1}\nSubClasse Prod: {2}\nID do Armazem: {3}\nData de Início: {4}\nData de Fim: {5}\nValor total a ser pago: {6}\nStatus do pedido: {7}", nome, classe, subclasse, idarmazem, dtInicio, dtFim, valorTotal, "Não pago"), "Daniel");
                        em.SubmeterEmail();
                        TempData["certo"] = msg;
                        return View("Index");
                    }
                    else
                    {
                        dias = Negocio.retornaDiferencaEntreDuasDatas(dtFim, dtInicio);
                        valorTotal = Convert.ToInt32(((dias * 10) * qtdarmazem) + 1000);
                        string msg = "Valor total a ser pago e: " + valorTotal;
                        TempData["certo"] = msg;
                        return View();
                    }

                }
                else
                {
                    TempData["erro"] = "Falha na simulacao! Voce selecionou mais espaco que o disponivel.";
                    return View();
                }

            }
            catch (Exception ex)
            {
                TempData["erro"] = "Falha na simulação. Erro: " + ex.Message;
                return View();
            }
        }
        [HttpGet]
        public JsonResult trazerSubClasse(int idClasse)
        {
            SubClasseRepository subClasseRepository = new SubClasseRepository();
            var listaSubClasses = subClasseRepository.Listar().Where(x => x.classeId == idClasse);
            return Json(listaSubClasses, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaineldeControleUsuario()
        {
            return View();
        }

        public ActionResult PaineldeControleAdmin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PaineldeControleAdmin(int pedidoId)
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            
            PedidoRepository pedidoRepository = new PedidoRepository();
            var pedido = pedidoRepository.Buscar(pedidoId);
            var cliente = clienteRepository.Buscar(pedido.clienteId);
            pedido.ckstatus = true;
            pedidoRepository.Update(pedido);
            EnviarMensagem em = new EnviarMensagem("daniel.hpassos@gmail.com", cliente.email, "Vindo do Sistema de Armazem", String.Format("Olá {0},\n\nSeu pedido acaba de ter seu status alterado para PAGO!\n\nOs dados do seu pedido são,\nCliente: {0}\nClasse Prod: {1}\nSubClasse Prod: {2}\nData de Início: {3}\nData de Fim: {4}\nValor total a ser pago: {5}\nStatus do pedido: {6}", cliente.nome, pedido.classeId, pedido.subclasseId, pedido.dtInicio, pedido.dtFim, pedido.valorTotal, "Pago"), "Daniel");
            em.SubmeterEmail();

            return Json(new { mensagem = "Status de pagamento alterado com sucesso!" });
        }

        [HttpGet]
        public JsonResult trazerPedidosUsuario()
        {
            List<PedidoView> ListaPedidoView = new List<PedidoView>();
            PedidoRepository pedidoRepository = new PedidoRepository();

            foreach (var item in pedidoRepository.Listar().Where(x => x.clienteId == ((Cliente)Session["User"]).clienteId))
            {
                PedidoView pedido = new PedidoView()
                {
                    pedidoId = item.pedidoId,
                    clienteId = item.clienteId,
                    classeId = item.classeId,
                    subclasseId = item.subclasseId,
                    armazenagemId = item.armazenagemId,
                    armazemId = item.armazemId,
                    dtInicio = item.dtInicio.ToShortDateString(),
                    dtFim = item.dtFim.ToShortDateString(),
                    valorTotal = item.valorTotal,
                    ckstatus = item.ckstatus
                };

                ListaPedidoView.Add(pedido);
            }

            return Json(ListaPedidoView, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult trazerPedidosAdmin()
        {
            List<PedidoView> ListaPedidoView = new List<PedidoView>();
            PedidoRepository pedidoRepository = new PedidoRepository();
            foreach (var item in pedidoRepository.Listar())
            {
                PedidoView pedido = new PedidoView()
                {
                    pedidoId = item.pedidoId,
                    clienteId = item.clienteId,
                    classeId = item.classeId,
                    subclasseId = item.subclasseId,
                    armazenagemId = item.armazenagemId,
                    armazemId = item.armazemId,
                    dtInicio = item.dtInicio.ToShortDateString(),
                    dtFim = item.dtFim.ToShortDateString(),
                    valorTotal = item.valorTotal,
                    ckstatus = item.ckstatus
                };

                ListaPedidoView.Add(pedido);
            }
            return Json(ListaPedidoView, JsonRequestBehavior.AllowGet);
        }
    }
}