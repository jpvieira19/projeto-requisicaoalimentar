namespace Domain.Model;

public class Pedidos : IPedidos
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

        public Pedidos() { }

        public Pedidos(long id, string name, int fiambre, int queijo, int bolo, DateTime date, string service, string responsavel, string emailBody, DateTime dataNecessidade)
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

        public Pedidos(long id)
        {
            Id = id;
        }
    }