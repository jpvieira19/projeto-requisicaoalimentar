namespace Domain.IRepository;

using Domain.Model;

public interface IPedidosRepository : IGenericRepository<Pedidos>
{
    Task<bool> PedidosExists(long id);
    Task<Pedidos> AddPedidos(Pedidos pedidos);
    Task<Pedidos> GetPedidosByIdAsync(long id);
}
