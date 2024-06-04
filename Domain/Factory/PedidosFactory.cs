namespace Domain.Factory;

using Domain.Model;

public class PedidosFactory: IPedidosFactory
{
    public Pedidos NewPedidos(long id)
    {
        return new Pedidos(id);
    }
}