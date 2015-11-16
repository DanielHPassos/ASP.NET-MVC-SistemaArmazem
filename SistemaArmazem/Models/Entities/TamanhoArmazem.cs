using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaArmazem.Models.Entities
{
    public class TamanhoArmazem
    {
        //TamanhoArmazem, é o armazém em questão. Com essa classe, podemos ter mais de 1 armazém, e redimensioná-lo à vontade
        public int tamanhoArmazemId { get; set; }
        public int tamanhoArmazem { get; set; }
    }
}