using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using gerenciador_de_tarefas_.Controllers;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using gerenciador_de_tarefas_.Dtos;
using gerenciador_de_tarefas_.Models;
using Microsoft.AspNetCore.Http;
using gerenciador_de_tarefas_.Services;
using Microsoft.AspNetCore.Authorization;

namespace gerenciador_de_tarefas_.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UsuarioController : BaseController
  {
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    [AllowAnonymous]
    

    public IActionResult SalvarUsuario([FromBody]Usuario usuario)
    {
      try
      {
        return Ok(usuario);

      }
      catch (Exception e)
      {
        _logger.LogError("Ocorreu erro ao salvar usuário", e);
        return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostaDto()
        {
          Status = StatusCodes.Status500InternalServerError,
          Erro = "Ocorreu erro ao salvar, Tente novamente!"
        });
      }
    }
  }
}