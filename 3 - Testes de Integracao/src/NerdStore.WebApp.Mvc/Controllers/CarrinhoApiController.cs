using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Queries;
using NerdStore.WebApp.Mvc.Models;
using NerdStore.WebApp.Mvc.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Mvc.Controllers
{
    [Authorize]
    public class CarrinhoApiController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IPedidoQueries _pedidoQueries;
        private readonly IMediatorHandler _mediatorHandler;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public CarrinhoApiController(
            INotificationHandler<DomainNotification> notifications,
            IProdutoAppService produtoAppService,
            IPedidoQueries pedidoQueries,
            IMediatorHandler mediatorHandler,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings)
            : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _produtoAppService = produtoAppService;
            _pedidoQueries = pedidoQueries;
            _mediatorHandler = mediatorHandler;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpGet("api/carrinho")]
        public async Task<IActionResult> Get()
        {
            return Response(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost("api/carrinho")]
        public async Task<IActionResult> Post([FromBody] ItemViewModel item)
        {
            var produto = await _produtoAppService.ObterPorId(item.Id);

            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < item.Quantidade)
            {
                NotificarErro("ErroValidacao", "Produto com estoque insuficiente.");
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, item.Quantidade, produto.Valor);

            await _mediatorHandler.EnviarComando(command);

            return Response();
        }

        [HttpPut("api/carrinho/{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ItemViewModel item)
        {
            var produto = await _produtoAppService.ObterPorId(id);

            if (produto == null) return BadRequest();

            var command = new AtualizarItemPedidoCommand(ClienteId, produto.Id, item.Quantidade);

            await _mediatorHandler.EnviarComando(command);

            return Response();
        }

        [HttpDelete("api/carrinho/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await _produtoAppService.ObterPorId(id);

            if (produto == null) return BadRequest();

            var command = new RemoverItemPedidoCommand(ClienteId, produto.Id);

            await _mediatorHandler.EnviarComando(command);

            return Response();
        }

        [AllowAnonymous, HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, false);

            if (result.Succeeded) return Ok(await GerarJwt(login.Email));

            NotificarErro("login", "Usuário ou senha incorreto.");

            return Response();
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var tokenResult = tokenHandler.WriteToken(token);

            return tokenResult;
        }
    }
}
