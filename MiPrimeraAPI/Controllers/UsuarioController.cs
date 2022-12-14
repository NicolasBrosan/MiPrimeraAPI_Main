using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;
using MiPrimeraAPI.Controllers.DTOS;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return new List<Usuario>();
        }

        [HttpDelete]
        public bool EliminarUsuario([FromBody] int id)
        {
            return UsuarioHandler.DeleteUser(id);
        }

        [HttpPost]
        public bool CrearNuevoUsuario([FromBody] PostUsuario usuario)
        {
            return UsuarioHandler.CreateNewUser(new Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail,
            });
        }

        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.UpdateUser(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail

            });
        }

    }
}
