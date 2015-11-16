using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaArmazem.Models.Entities
{
    public class Armazenagem
    {
        //Armazenagem representa o tipo de produto que será armazenado
        public int      armazenagemId { get; set; }
        public string   tpArmazenagem { get; set; }
    }
}