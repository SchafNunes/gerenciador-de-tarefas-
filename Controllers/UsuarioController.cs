using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using gerenciador_de_tarefas_.Controllers;
using System.Text.RegularExpressions;
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


    public IActionResult SalvarUsuario([FromBody] Usuario usuario)
    {
      try
      {
        var erros = new List<string>();
        if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Nome)
            || usuario.Nome.Length < 2)
        {
          erros.Add("Nome inválido");
        }

        if (string.IsNullOrEmpty(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.Senha)
            || usuario.Senha.Length < 4 && Regex.IsMatch(usuario.Senha, "[a-zA-Z0-9]+", RegexOptions.IgnoreCase))
        {
          erros.Add("Senha inválida");
        }

        Regex regex = new Regex(@"^([\w\.\-\+\d]+)@([\w\-]+)((\.(\w){2,4})+)$");
        if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Email)
            || !regex.Match(usuario.Email).Success)
        {
          erros.Add("Email inválido");
        }

        if (erros.Count > 0)
        {
          return BadRequest(new ErroRespostaDto()
          {
            Status = StatusCodes.Status400BadRequest,
            Erros = erros
          });
        }

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