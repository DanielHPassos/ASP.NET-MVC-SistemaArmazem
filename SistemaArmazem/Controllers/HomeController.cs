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
                        return View("Index");
                    else
                        return View("Painel");

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
        public ActionResult Simulacao(string classe, string subclasse, string armazenagem,int idarmazem ,int qtdarmazem,
            DateTime dtInicio, DateTime dtFim)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(classe) && string.IsNullOrWhiteSpace(subclasse) && string.IsNullOrWhiteSpace(subclasse) && string.IsNullOrWhiteSpace(armazenagem))
                {
                    TempData["erro"] = "Não deixe espaços em branco!";
                    return View();
                }
                if(DateTime.Now.Day > dtInicio.Day)
                {
                    TempData["erro"] = "A data de inicio não pode ser menor que a data de hoje!";
                    return View();
                }
                if (dtFim.Day <= dtInicio.Day)
                {
                    TempData["erro"] = "A data de Fim não pode ser menor que a data de Inicio!";
                    return View();
                }
                if(qtdarmazem <= 0)
                {
                    TempData["erro"] = "Quantidade de espaço não pode ser zero ou vazia!";
                    return View();
                }
                if (Negocio.armazemTemEspacoPraIsso(qtdarmazem,idarmazem))
                {
                    var dias = dtFim - dtInicio;
                    int valorTotal = Convert.ToInt32(((dias.TotalDays*10) * qtdarmazem)+1000);
                    string msg = "Valor total a ser pago e: "+ valorTotal;
                    TempData["certo"] = msg;
                    return View();
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
            return Json(listaSubClasses , JsonRequestBehavior.AllowGet);
        }
    }
}