

using System.Collections.Generic;
using System.Threading.Tasks;
using APIDataDriven.Data;
using APIDataDriven.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Aplicações de ASP.NET Core por padrão utilizam as portas: 
//http 5000
//https 5001
//exemplo de url na azure (https://meuapp.azurewebsites.net/)
namespace APIDataDriven.Controllers
{
  [Route("v1/categories")]
  public class CategoryController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
    public Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
    {
      //var categories = await context.Categories.AsNoTracking().ToListAsync();
      //return categories;
      return null;
    }

  }
}