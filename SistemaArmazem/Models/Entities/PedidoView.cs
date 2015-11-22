namespace SistemaArmazem.Models.Entities
{
    public class PedidoView
    {
        public int pedidoId { get; set; }
        public int clienteId { get; set; }
        public int classeId { get; set; }
        public int subclasseId { get; set; }
        public int armazenagemId { get; set; }
        public int armazemId { get; set; }
        public string produto { get; set; }
        public string dtInicio { get; set; }
        public string dtFim { get; set; }
        public decimal valorTotal { get; set; }
        public bool ckstatus { get; set; }
    }
}
