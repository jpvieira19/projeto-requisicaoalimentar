namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

public interface IAbsanteeContext
{
	DbSet<PedidosDataModel> Pedidos { get; set; }

}