using ClienteBlazorWASM.Models;
using ClienteBlazorWASM.Servicios.IServicio;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace ClienteBlazorWASM.Pages.Autenticacion
{
    public partial class Acceder
    {
        private UsuarioAutenticacion usuarioAutenticacion = new UsuarioAutenticacion();
        public bool EstaProcesando { get; set; } = false;
        public bool MostrarErroresAutenticacion { get; set; }
        public string UrlRetorno { get; set; }

        public string Errores { get; set; }

        [Inject]
        public IServicioAutenticacion servicioAutenticacion { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task AccesoUsuario()
        {
            MostrarErroresAutenticacion = false;
            EstaProcesando = true;
            var result = await servicioAutenticacion.Acceder(usuarioAutenticacion);
            if (result.IsSuccess)
            {
                EstaProcesando = false;
                var urlAbsoluta = new Uri(navigationManager.Uri);
                var parametrosQuery = HttpUtility.ParseQueryString(urlAbsoluta.Query);
                UrlRetorno = parametrosQuery["returnUrl"];
                if(string.IsNullOrEmpty(UrlRetorno))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + UrlRetorno);
                }
            }
            else
            {
                EstaProcesando = false;
                MostrarErroresAutenticacion = true;
                Errores = "Usuario y/o contraseña incorrectos";
                navigationManager.NavigateTo("/acceder");
            }
        }
    }
}
