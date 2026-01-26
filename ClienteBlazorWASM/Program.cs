using ClienteBlazorWASM;
using ClienteBlazorWASM.Servicios;
using ClienteBlazorWASM.Servicios.IServicio;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Agregar servicios aqui
builder.Services.AddScoped<IPostsServicio, PostsServicio>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
