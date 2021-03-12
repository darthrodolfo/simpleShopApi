using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDataDriven.Data;
using APIDataDriven.Models;
using APIDataDriven.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDataDriven.Controllers
{
  [Route("v1/users")]
  public class UserController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
    {
      var users = await context.Users.AsNoTracking().ToListAsync();
      return users;
    }

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<User>> Post([FromServices] DataContext context, [FromBody] User newUser)
    {
      if (ModelState.IsValid == false)
        return BadRequest(ModelState);

      try
      {
        newUser.Role = "employee";

        context.Users.Add(newUser);
        await context.SaveChangesAsync();

        newUser.Password = string.Empty;
        return newUser;
      }
      catch (System.Exception)
      {

        throw;
      }
    }

    [HttpGet]
    [Route("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<User>> Put([FromServices] DataContext context, int id, [FromBody] User user)
    {
      if (ModelState.IsValid == false)
        return BadRequest(ModelState);

      if (id != user.Id)
        return NotFound(new { message = "Usuario não encontrada" });

      try
      {
        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return user;
      }
      catch (Exception)
      {
        return BadRequest(new { message = "Não foi possível criar o usuário" });
      }

    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody] User user)
    {
      if (user == null)
        return BadRequest(new { message = "Usuário inválido" });

      var authenticatedUser = await context.Users.AsNoTracking()
      .Where(x => x.Username == user.Username && x.Username == user.Password)
      .FirstOrDefaultAsync();

      if (user == null)
        return NotFound(new { message = "Usuário ou senha inválidos" });

      var token = TokenService.GenerateToken(user);
      user.Password = string.Empty;

      return new
      {
        user = user,
        token = token
      };
    }


  }
}