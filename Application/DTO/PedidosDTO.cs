using Domain.Model;

namespace Application.DTO
{
    public class PedidosDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Fiambre { get; set; }
        public int Queijo { get; set; }
        public int Bolo { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; }
        public string Responsavel { get; set; }
        public string EmailBody { get; set; }
        public DateTime DataNecessidade { get; set; } // Novo campo adicionado

        public PedidosDTO() { }

        public PedidosDTO(long id, string name, int fiambre, int queijo, int bolo, DateTime date, string service, string responsavel, string emailBody, DateTime dataNecessidade)
        {
            Id = id;
            Name = name;
            Fiambre = fiambre;
            Queijo = queijo;
            Bolo = bolo;
            Date = date;
            Service = service;
            Responsavel = responsavel;
            EmailBody = emailBody;
            DataNecessidade = dataNecessidade; // Novo campo adicionado
        }

        public static PedidosDTO ToDTO(Pedidos pedidos)
        {
            if (pedidos == null)
            {
                throw new ArgumentException("pedidos must not be null");
            }

            return new PedidosDTO(
                pedidos.Id,
                pedidos.Name,
                pedidos.Fiambre,
                pedidos.Queijo,
                pedidos.Bolo,
                pedidos.Date,
                pedidos.Service,
                pedidos.Responsavel,
                pedidos.EmailBody,
                pedidos.DataNecessidade // Novo campo adicionado
            );
        }

        public static IEnumerable<PedidosDTO> ToDTO(IEnumerable<Pedidos> pedidoss)
        {
            List<PedidosDTO> pedidossDTO = new List<PedidosDTO>();

            foreach (Pedidos pedidos in pedidoss)
            {
                PedidosDTO pedidosDTO = ToDTO(pedidos);
                pedidossDTO.Add(pedidosDTO);
            }

            return pedidossDTO;
        }

        public static Pedidos ToDomain(PedidosDTO pedidosDTO)
        {
            if (pedidosDTO == null)
            {
                throw new ArgumentException("pedidosDTO must not be null");
            }

            return new Pedidos(
                pedidosDTO.Id,
                pedidosDTO.Name,
                pedidosDTO.Fiambre,
                pedidosDTO.Queijo,
                pedidosDTO.Bolo,
                pedidosDTO.Date,
                pedidosDTO.Service,
                pedidosDTO.Responsavel,
                pedidosDTO.EmailBody,
                pedidosDTO.DataNecessidade // Novo campo adicionado
            );
        }
    }
}
