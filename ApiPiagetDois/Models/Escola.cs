using System.ComponentModel.DataAnnotations;

namespace ApiPiagetDois.Models
{
    public class Escola : Base
    {
        [Required(ErrorMessage ="Informe o nome")]
        public string? EscolaNome { get; set; }

        [Required]
        public string? EscolaTelefone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Formato Email Invalido")]
        public string? EscolaEmail { get; set; }

    }
}
