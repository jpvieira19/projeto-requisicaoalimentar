using Domain.Model;

public class PedidosDataModel
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

    public PedidosDataModel() {}

    public PedidosDataModel(Pedidos pedidos)
    {
        if (pedidos == null)
        {
            throw new ArgumentException("pedidos must not be null");
        }

        Id = pedidos.Id;
        Name = pedidos.Name;
        Fiambre = pedidos.Fiambre;
        Queijo = pedidos.Queijo;
        Bolo = pedidos.Bolo;
        Date = pedidos.Date;
        Service = pedidos.Service;
        Responsavel = pedidos.Responsavel;
        EmailBody = pedidos.EmailBody;
        DataNecessidade = pedidos.DataNecessidade; // Novo campo adicionado
    }
}
