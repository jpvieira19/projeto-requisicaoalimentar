namespace Domain.Model;

public interface IPedidos
{
     long Id { get; set; }
        string Name { get; set; }
        int Fiambre { get; set; }
        int Queijo { get; set; }
        int Bolo { get; set; }
        DateTime Date { get; set; }
        string Service { get; set; }
        string Responsavel { get; set; }
        string EmailBody { get; set; }
        DateTime DataNecessidade { get; set; } // Novo campo adicionado
}