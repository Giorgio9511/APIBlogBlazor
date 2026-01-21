using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models.Dtos
{
    public class UsuarioDto
    {
        [Key]
        public int Id { get; set; }

        public string NombreUsuario { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }
    }
}
