using ApiBlog.Data;
using ApiBlog.Mappers;
using ApiBlog.Repository;
using ApiBlog.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration["Jwt:Key"];

//Configuración de la conexion sql server
builder.Services.AddDbContext< ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"));
});

// Add services to the container.
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Agregar automapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

//Aqui se configura la autenticación - primera parte
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiBlog",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticación JWT usando el esquema Bearer.\r\n\r\n" +
        "Escribe 'Bearer' [espacio] y luego tu token.\r\n\r\n" +
        "Ejemplo: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"ImagenesPosts")),
    RequestPath = new PathString("/ImagenesPosts")
});


//Soporte para CORS
app.UseCors("PolicyCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
