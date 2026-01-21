using ApiBlog.Data;
using ApiBlog.Models;
using ApiBlog.Models.Dtos;
using ApiBlog.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiBlog.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _bd;
        private string claveSecreta;

        public UsuarioRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public Usuario GetUsuario(int id)
        {
            return _bd.Usuario.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuario.OrderBy(u => u.Id).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuarioBd = _bd.Usuario.FirstOrDefault(u => u.NombreUsuario == usuario);
            if(usuarioBd == null)
                return true;

            return false;
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenermd5();
            var usuario = _bd.Usuario.FirstOrDefault(
                u => u.NombreUsuario.ToLower() == usuarioLoginDto.NombreUsuario.ToLower()
                && u.Password == passwordEncriptado
                );

            //Validamos si el usuario no existe con la combinación de usuario y contraseña correcta
            if (usuario == null)
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }

            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()),
                    //new Claim(ClaimTypes.Role, usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
        }

        public Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            throw new NotImplementedException();
        }
    }
}
