using ClienteBlazorWASM.Models;

namespace ClienteBlazorWASM.Servicios.IServicio
{
    public interface IServicioAutenticacion
    {
        Task<RespuestaRegistro> RegistrarUsuario(UsuarioRegistro usuarioParaRegistro);
        Task<RespuestaAutenticacion> Acceder(UsuarioAutenticacion usuarioParaAutenticar);
        Task Salir();
    }
}
