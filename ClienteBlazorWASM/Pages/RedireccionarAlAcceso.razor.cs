using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClienteBlazorWASM.Pages
{
    public partial class RedireccionarAlAcceso
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> estadoProveedorAutenticacion { get; set; }
        bool noAuthorizado { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var estadoAutorizacion = await estadoProveedorAutenticacion;

            if(estadoAutorizacion == null)
            {
                var returnUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);
                if(string.IsNullOrEmpty(returnUrl))
                {
                    navigationManager.NavigateTo("Acceder", true);
                }
                else
                {
                    navigationManager.NavigateTo($"Acceder?returnUrl={returnUrl}");
                }
            }
            else
            {
                noAuthorizado = true;
            }
        }
    }
}
