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
  public class LoginController : BaseController
  {

    private readonly string loginTeste = "teste@teste.com";
    private readonly string senhaTeste = "1234";
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    // [AllowAnonymous] 
    public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto requisicao)
    {
      try
      {
        if (requisicao == null || requisicao.Login == null || requisicao.Senha == null
        || string.IsNullOrEmpty(requisicao.Login) || string.IsNullOrWhiteSpace(requisicao.Login)
        || string.IsNullOrEmpty(requisicao.Senha) || string.IsNullOrWhiteSpace(requisicao.Senha)
        || requisicao.Login != loginTeste || requisicao.Senha != senhaTeste)
        {
          return BadRequest(new ErroRespostaDto()
        {
          Status = StatusCodes.Status400BadRequest,
          Erro = "Parametros de entrada Inválidos"
        });
        }
        var usuarioTeste = new Usuario()
        {
          Id = 1,
          Nome = "Usuário de Testes",
          Email = loginTeste,
          Senha = senhaTeste,
        };

         var token = TokenService.CriarToken(usuarioTeste);

        return Ok(new LoginRetornoDto() {
          Email = usuarioTeste.Email,
          Nome = usuarioTeste.Nome,
          Token = token
        }); 
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