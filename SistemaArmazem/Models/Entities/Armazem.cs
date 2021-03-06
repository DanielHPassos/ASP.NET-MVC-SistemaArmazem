﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaArmazem.Models.Entities
{
    public class Armazem
    {
        //Armazém representa uma pequena parte de um armazém
        public int      armazemId { get; set; }
        public int      clienteId { get; set; }
        public int      usadoArmazem { get; set; }
        public int      tamanhoArmazemId { get; set; }

        [ForeignKey("clienteId")]
        public Cliente cliente { get; set; }

        [ForeignKey("tamanhoArmazemId")]
        public TamanhoArmazem tamanhoArmazem { get; set; }
    }
}