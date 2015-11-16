using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaArmazem.Models.Entities
{
    public class Cliente
    {
        //Cliente conta com os dados do cliente. Tanto cliente como Admin
        public int      clienteId { get; set; }
        public string   nome { get; set; }
        public string   razao { get; set; }
        public string   titular { get; set; }
        public string   login { get; set; }
        public string   senha { get; set; }
        public string   email { get; set; }
        public string   telefone { get; set; }
        public string   celular { get; set; }
        public string   cep { get; set; }
        public string   rua { get; set; }
        public string   bairro { get; set; }
        public string   cidade { get; set; }
        public string   estado { get; set; }
        public string   pais { get; set; }
        public string   numero { get; set; }
        public string   grupo { get; set; }
    }
}