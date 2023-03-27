using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lib.Domain.Entities
{
    public class TodoTask: EntityBase
    {
        [Required (ErrorMessage ="El id es obligatorio, si es un valor nuevo dejar en 0")]
        [DefaultValue(0)]
        public int TaskId { get; set; }

        [Required (AllowEmptyStrings =false, ErrorMessage = "El título es obligatorio")]
        [StringLength(100, MinimumLength =3, ErrorMessage = "El título debe tener máximo 100 caracteres y mínimo 3")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción es obligatoria")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "La descripción debe tener máximo 100 caracteres y mínimo 3")]
        public string? Description { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="El campo completo es obligatorio")]
        public bool Completed { get; set; }
    }
}
