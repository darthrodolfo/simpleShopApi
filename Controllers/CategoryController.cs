

using System.Collections.Generic;
using System.Threading.Tasks;
using APIDataDriven.Data;
using APIDataDriven.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
    {
      var categories = await context.Categories.AsNoTracking().ToListAsync();
      return categories;
    }

    public async Task<ActionResult<Category>> Post([FromServices] DataContext context,
     [FromBody] Category model)
    {
      if (ModelState.IsValid == false)
        return BadRequest(ModelState);

      try
      {
        context.Categories.Add(model);
        await context.SaveChangesAsync();
        return model;
      }
      catch (System.Exception ex)
      {
        return BadRequest(new { message = "Não foi possível criar uma nova categoria" });
      }

    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> Put([FromServices] DataContext context, int id, [FromBody] Category model)
    {
      if (id != model.Id)
        return NotFound(new { message = "Categoria não encontrada" });

      if (ModelState.IsValid == false)
        return BadRequest(ModelState);

      try
      {
        context.Entry<Category>(model).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return model;
      }
      catch (DbUpdateConcurrencyException)
      {
        return BadRequest(new { message = "Não foi possível atualizar a categoria" });
      }
    }

    public async Task<ActionResult<Category>> Delete(
      [FromServices] DataContext context, int id)
    {
      var category = await context.Categories.FirstOrDefaultAsync(categ => categ.Id == id);
      if (category == null)
        return NotFound(new { message = "Categoria não encontrada" });

      try
      {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return category;
      }
      catch (System.Exception)
      {
        return BadRequest(new { message = "Não foi possível remover a categoria" });
      }
    }

  }
}