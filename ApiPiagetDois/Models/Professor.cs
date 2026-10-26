using System.ComponentModel.DataAnnotations;

namespace ApiPiagetDois.Models
{
    public class Professor : Base
    {
        [Required(ErrorMessage ="Informe o nome")]
        public string? ProfessorNome { get; set; }

        [Required]
        public string? ProfessorTelefone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Formato Email Invalido")]
        public string? ProfessorEmail { get; set; }

        [Required]
        public string? ProfessorDisciplina { get; set; }

    }
}
