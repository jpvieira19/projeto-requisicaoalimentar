using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Services;
using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {   
        private readonly PedidosService _pedidosService;
        private readonly EmailService _emailService;

        public PedidosController(PedidosService pedidosService, EmailService emailService)
        {
            _pedidosService = pedidosService;
            _emailService = emailService;
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<PedidosDTO>> PostPedidos(PedidosDTO pedidosDTO)
        {
            Console.WriteLine("post pedidos");
            List<string> errorMessages = new List<string>();
            PedidosDTO pedidosResultDTO = await _pedidosService.Add(pedidosDTO, errorMessages);
            if (pedidosResultDTO != null)
            {
                return Ok(pedidosResultDTO);
            }
            else
            {
                return BadRequest(errorMessages);
            }
        }

        // POST: api/Pedidos/send-email
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmailWithPdf(IFormFile pdf, [FromForm] string pedido)
        {
            if (pdf == null || pdf.Length == 0)
                return BadRequest("No PDF file provided.");

            PedidosDTO pedidoDTO;
            try
            {
                pedidoDTO = JsonConvert.DeserializeObject<PedidosDTO>(pedido);
                Console.WriteLine("Pedido recebido: " + pedido); // Log para debug
                Console.WriteLine("EmailBody recebido: " + pedidoDTO.EmailBody); // Log para debug
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao desserializar o pedido: " + ex.Message); // Log para debug
                return BadRequest("Invalid pedido data.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await pdf.CopyToAsync(memoryStream);
                var pdfBytes = memoryStream.ToArray();

                var result = await _emailService.SendEmailWithPdfAsync(pedidoDTO, pdfBytes);
                if (result)
                {
                    return Ok("Email sent successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to send email.");
                }
            }
        }

    }
}
