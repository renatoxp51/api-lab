using LabReserva.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using LabReserva.Data;

namespace LabReserva.Services
{
    public class TokenService
    {
        private SecurityTokenDescriptor tokenDescriptor;

        // método para gerar o token
        public string Generate(Usuario usuario)
        {
            //criar uma instância do JwtSecurityTokenHandler (esse objeto será o principal responsável por gerar o token)
            var handler = new JwtSecurityTokenHandler();

            // pegando a chave que está na classe Configuration codificando em uma cadeia de bytes
            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

            // criando as credenciais
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            // configurando qual a forma que o token vai ser gerado
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(usuario),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(8)
            };

            // gerar o token
            var token = handler.CreateToken(tokenDescriptor);

            // gerar uma string do token
            return handler.WriteToken(token);

        }

        // método para gerar os claims
        private static ClaimsIdentity GenerateClaims(Usuario usuario) 
        {

            var ci = new ClaimsIdentity();

            ci.AddClaim(
                new Claim(
                    ClaimTypes.Name,
                    usuario.EmailUsuario
                ));

            // adicionadn Claim para autorizar usuários pelo tipo.
            ci.AddClaim(
                new Claim(
                    ClaimTypes.Role, 
                    usuario.IdTipoUsuario.ToString()
                ));


            return ci;

        }
    }
}
