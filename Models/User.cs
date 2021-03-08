using System.ComponentModel.DataAnnotations;

namespace Backoffice.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
    public int Username { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
    public int Password { get; set; }

    public int Role { get; set; }
  }
}