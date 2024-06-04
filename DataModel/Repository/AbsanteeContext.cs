namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;

public class AbsanteeContext : DbContext
{
	protected readonly IConfiguration Configuration;

	//public AbsanteeContext() {}
	public AbsanteeContext(DbContextOptions<AbsanteeContext> options)
		: base(options)
	{
		Database.EnsureCreated();
	}

	public virtual DbSet<PedidosDataModel> Pedidos { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		// necessário se Domain.Model.Colaborator fosse usado para persistência, e se pretendesse que os atributos/propriedades fossem privadas

		// var property = typeof(Colaborator).GetProperty("Name", BindingFlags.NonPublic | BindingFlags.Instance);
		// if (property != null)
		// 	modelBuilder.Entity<Colaborator>()
		// 		.Property("_strName")
		// 		.HasColumnName("Name");

		// modelBuilder.Entity<Colaborator>()
        // 	.HasKey(c => c.Id);
			
		// modelBuilder.Entity<Colaborator>()
        // 	.HasKey(c => c.Email);

	}
}