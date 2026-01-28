using ClienteBlazorWASM.Servicios.IServicio;
using Microsoft.AspNetCore.Components;

namespace ClienteBlazorWASM.Pages.Autenticacion
{
    public partial class Salir
    {
        [Inject]
        public IServicioAutenticacion servicioAutenticacion { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await servicioAutenticacion.Salir();
            navigationManager.NavigateTo("/");
        }
    }
}
