using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string? RutaImagen { get; set; }

        [Required]
        public string Etiquetas { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

    }
}
