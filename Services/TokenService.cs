using System;
using System.Collections.Generic;
using gerenciador_de_tarefas_.Models;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace gerenciador_de_tarefas_.Services
{
  public class TokenService
  {
    public static string CriarToken(Usuario usuario) 
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var chaveCriptografiaEmBytes = Encoding.ASCII.GetBytes(ChaveJWT.ChaveSecreta);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, usuario.Nome)
        }),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveCriptografiaEmBytes), SecurityAlgorithms.EcdsaSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}