using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerenciador_de_tarefas_.Dtos
{
  public class LoginRetornoDto
  {
    public string Email { get; set; }
    public string Nome { get; set; }
    public string Token { get; set; }
  }
}

