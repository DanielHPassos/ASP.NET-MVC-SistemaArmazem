﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaArmazem.Models.Entities
{
    public class SubClasse
    {
        //SubClasse representa a sub categoria da Classe do produto
        public int      subclasseId { get; set; }
        public int      classeId { get; set; }
        public string   nmSubClasse { get; set; }

        [ForeignKey("classeId")]
        public Classe classe { get; set; }
    }
}