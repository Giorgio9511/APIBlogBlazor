using System.ComponentModel.DataAnnotations;

namespace ClienteBlazorWASM.Models.ViewModels
{
    public class PostActualizarVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        public string? RutaImagen { get; set; }

        [Required(ErrorMessage = "Las etiquetas son obligatorias")]
        public string Etiquetas { get; set; }
    }
}
