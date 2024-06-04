namespace Domain.Factory;

using Domain.Model;

public interface IPedidosFactory
{
    Pedidos NewPedidos(long id);
}