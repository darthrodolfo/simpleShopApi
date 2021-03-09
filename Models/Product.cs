using System.ComponentModel.DataAnnotations;

namespace APIDataDriven.Models
{
  public class Product
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Erro campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
    public string Title { get; set; }

    [MaxLength(1024, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Erro campo é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Erro campo é obrigatório")]

    public int CategoryId { get; set; }

    public Category Category { get; set; }

  }
}