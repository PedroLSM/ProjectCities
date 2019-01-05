using System.ComponentModel.DataAnnotations;

namespace CitiesInfo.API.Models
{
    public class PointOfInterestForCreateDTO
    {
        [Required(ErrorMessage = "Nome é requerido.")]
        [MaxLength(50, ErrorMessage = "Nome tem que ter no máximo 50 caracteres.")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Descrição tem que ter no máximo 100 caracteres.")]
        public string Description { get; set; }
    }
}