using System.ComponentModel.DataAnnotations;

namespace Bank.Core.Requests.Categories;

public class CreateCategoryRequest : Request
{

  [Required(ErrorMessage = "Título Inválido")]
  [MaxLength(80, ErrorMessage = "O titulo deve conter até 80 caracteres")]
  public string Title {get; set;} = string.Empty;

  [Required(ErrorMessage = "Descrição Inválida")]
  public string Description {get; set;} = string.Empty;

}