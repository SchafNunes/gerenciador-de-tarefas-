using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerenciador_de_tarefas_.Dtos
{
  public class ErroRespostaDto
  {
    public int Status { get; set; }
    public string Erro { get; set; }
    public List<string> Erros { get; set; }


  }
}

