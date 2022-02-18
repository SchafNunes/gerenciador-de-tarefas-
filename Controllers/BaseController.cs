using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
 
namespace gerenciador_de_tarefas_.Controllers
{
  [Authorize]
   public class BaseController : ControllerBase
   {
      
   }  
}