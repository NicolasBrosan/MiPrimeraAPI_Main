using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOS;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public bool NuevaVenta([FromBody] PostVenta venta)
        {
            return VentaHandler.CreateNewSale(new Venta
            {
                Comentarios = venta.Comentarios
            });
        }
    }

}
