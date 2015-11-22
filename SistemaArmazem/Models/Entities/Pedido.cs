using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaArmazem.Models.Entities
{
    public class Pedido
    {
        //Pedido representa os dados do pedido em si, sendo eles...
        public int          pedidoId        { get; set; }                   //Numeração do pedido
        public int          clienteId       { get; set; }                   //Numeração do cliente
        public int          classeId        { get; set; }                   //Classe do produto
        public int          subclasseId     { get; set; }                   //SubClasse do produto
        public int          armazenagemId   { get; set; }                   //Tipo de armazenagem do produto
        public int          armazemId       { get; set; }                   //Espaço que está sendo alocado nesse pedido¹
        public string       produto         { get; set; }                   //Nome do produto
        public DateTime     dtInicio        { get; set; }                   //Data de início do armazenamento
        public DateTime     dtFim           { get; set; }                   //Data de fim do armazenamento
        public decimal      valorTotal      { get; set; }                   //Valor total pago
        public bool         ckstatus        { get; set; }                   //Status marca se o Admin aprovou a solicitação²

        [ForeignKey("clienteId")]
        public Cliente cliente { get; set; }

        [ForeignKey("classeId")]
        public Classe classe { get; set; }

        [ForeignKey("subclasseId")]
        public SubClasse subclasse { get; set; }

        [ForeignKey("armazenagemId")]
        public Armazenagem armazenagem { get; set; }

        [ForeignKey("armazemId")]
        public Armazem armazem { get; set; }
    }
}
//  ¹Cada pedido pode alocar somente produtos do mesmo tipo. Para tipos diferentes, tem de ser criados mais pedidos.
//  Foi feito dessa forma, a fim de minimizar o sistema.
//
//  ²Se o ckstatus(Status) marcar 1, significa TRUE, ou seja, a armazenagem foi paga.