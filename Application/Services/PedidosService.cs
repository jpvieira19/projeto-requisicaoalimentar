namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Microsoft.EntityFrameworkCore;
using DataModel.Repository;
using Domain.IRepository;
using Domain.Factory;

public class PedidosService {

    private readonly AbsanteeContext _context;

    private readonly IPedidosRepository _pedidosRepository;


    
    public PedidosService( IPedidosRepository pedidosRepository) {
        
        _pedidosRepository = pedidosRepository;
    }    

    public async Task<PedidosDTO> Add(PedidosDTO pedidosDto, List<string> errorMessages)
    {
        
        bool bExists = await _pedidosRepository.PedidosExists(pedidosDto.Id);
        if(bExists) {
            Console.WriteLine("entrou bexists");
            errorMessages.Add("pedido already exists");
            return null;
        }
        try{
            Pedidos pedidos = PedidosDTO.ToDomain(pedidosDto);

            pedidos = await _pedidosRepository.AddPedidos(pedidos);
            
            PedidosDTO pedidosDTO = PedidosDTO.ToDTO(pedidos);
            
            return pedidosDTO;
        }catch (Exception ex)
        {
            Console.WriteLine($"Error adding pedidos: {ex.Message}");
            errorMessages.Add("An error occurred while adding the pedidos.");
            return null;
        }
    }

    

    
}