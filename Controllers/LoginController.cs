using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using gerenciador_de_tarefas_.Dtos;
using Microsoft.AspNetCore.Http;

namespace gerenciador_de_tarefas_.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LoginController : ControllerBase
  {
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto requisicao)
    {
      try
      {
        if (requisicao == null || requisicao.Login == null || requisicao.Senha == null)
        {
          return BadRequest(new ErroRespostaDto()
        {
          Status = StatusCodes.Status400BadRequest,
          Erro = "Parametros de entrada Inválidos"
        });
        }
        return Ok("Usuário autenticado com sucesso");
      }
      catch (Exception execao)
      {
        _logger.LogError($"ocorreu erro ao efetuar login {execao.Message}", execao);
        return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostaDto()
        {
          Status = StatusCodes.Status500InternalServerError,
          Erro = "Ocorreu erro ao efetuar login, Tente novamente!"
        });
      }
    }
  }
}