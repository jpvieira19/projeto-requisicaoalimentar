namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

using DataModel.Mapper;

using Domain.Model;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class PedidosRepository : GenericRepository<Pedidos>, IPedidosRepository
{    
    PedidosMapper _pedidosMapper;
    public PedidosRepository(AbsanteeContext context, PedidosMapper mapper) : base(context!)
    {
        _pedidosMapper = mapper;
    }

    public async Task<Pedidos> GetPedidosByIdAsync(long id)
        {
            try {
                PedidosDataModel pedidosDataModel = await _context.Set<PedidosDataModel>()
                        .FirstAsync(c => c.Id==id);

                Pedidos pedidos = _pedidosMapper.ToDomain(pedidosDataModel);

                return pedidos;
            }
            catch
            {
                throw;
            }
        }


    public async Task<Pedidos> AddPedidos(Pedidos pedidos)
    {
        try {

            PedidosDataModel pedidosDataModel = _pedidosMapper.ToDataModel(pedidos);

            EntityEntry<PedidosDataModel> pedidosDataModelEntityEntry = _context.Set<PedidosDataModel>().Add(pedidosDataModel);
            
            await _context.SaveChangesAsync();

            PedidosDataModel pedidosDataModelSaved = pedidosDataModelEntityEntry.Entity;

            Pedidos pedidosSaved = _pedidosMapper.ToDomain(pedidosDataModelSaved);

            return pedidosSaved;    
        }
        catch
        {
            throw;
        }
    }

    
    public async Task<bool> PedidosExists(long id)
    {
    Console.WriteLine($"Checking if Pedidos with ID {id} exists");
    var exists = await _context.Set<PedidosDataModel>().AnyAsync(e => e.Id == id);
    Console.WriteLine($"Exists: {exists}");
    return exists;
    }
}