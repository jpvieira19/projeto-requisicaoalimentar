using Domain.Factory;
using Domain.Model;

namespace DataModel.Mapper
{
    public class PedidosMapper
    {
        private IPedidosFactory _pedidosFactory;

        public PedidosMapper(IPedidosFactory pedidosFactory)
        {
            _pedidosFactory = pedidosFactory;
        }

        public Pedidos ToDomain(PedidosDataModel pedidosDM)
        {
            if (pedidosDM == null)
            {
                throw new ArgumentException("pedidosDM must not be null");
            }

            Pedidos pedidosDomain = new Pedidos(
                pedidosDM.Id,
                pedidosDM.Name,
                pedidosDM.Fiambre,
                pedidosDM.Queijo,
                pedidosDM.Bolo,
                pedidosDM.Date,
                pedidosDM.Service,
                pedidosDM.Responsavel,
                pedidosDM.EmailBody,
                pedidosDM.DataNecessidade // Novo campo adicionado
            );

            return pedidosDomain;
        }

        public IEnumerable<Pedidos> ToDomain(IEnumerable<PedidosDataModel> pedidossDM)
        {
            if (pedidossDM == null)
            {
                throw new ArgumentException("pedidossDM must not be null");
            }

            List<Pedidos> pedidossDomain = new List<Pedidos>();

            foreach (PedidosDataModel pedidosDataModel in pedidossDM)
            {
                Pedidos pedidosDomain = ToDomain(pedidosDataModel);
                pedidossDomain.Add(pedidosDomain);
            }

            return pedidossDomain.AsEnumerable();
        }

        public PedidosDataModel ToDataModel(Pedidos pedidos)
        {
            if (pedidos == null)
            {
                throw new ArgumentException("pedidos must not be null");
            }

            var pedidosDataModel = new PedidosDataModel
            {
                Id = pedidos.Id,
                Name = pedidos.Name,
                Fiambre = pedidos.Fiambre,
                Queijo = pedidos.Queijo,
                Bolo = pedidos.Bolo,
                Date = pedidos.Date,
                Service = pedidos.Service,
                Responsavel = pedidos.Responsavel,
                EmailBody = pedidos.EmailBody,
                DataNecessidade = pedidos.DataNecessidade // Novo campo adicionado
            };

            return pedidosDataModel;
        }
    }
}
