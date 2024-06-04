using Domain.IRepository;

namespace UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository Generic { get; }

    IPedidosRepository PedidosRepository { get; }
    

    Task<int> CompleteAsync();
}
