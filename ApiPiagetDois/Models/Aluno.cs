using System.ComponentModel.DataAnnotations;

namespace ApiPiagetDois.Models
{
    /// <summary>
    /// Classe aluno
    /// </summary>
    public class Aluno : Base
    {
        [Required (ErrorMessage ="Informe o nome")]
        public string? AlunoNome { get; set; }

        [Required]
        public string? AlunoTelefone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Formato Email Invalido")]
        public string? AlunoEmail { get; set; }


        [Required]
        public string? AlunoMatricula { get; set; }
    }
}
