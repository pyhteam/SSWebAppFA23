using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : Controller
	{
		private readonly AccountRepository _accountRepository;

        public AuthController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

		#region Register
		/// <summary>
		/// Register
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("Register")]
		public IActionResult Register(string username, string password)
		{
			var account = new Account()
			{
				Username = username,
				Password = password,
				Role = AccountRole.Customer,
				Status = AccountStatus.Active
			};
			try
			{
				// Check if username is existed
				var existedAccount = _accountRepository.Get(x => x.Username == username);
				if (existedAccount != null)
				{
					return new JsonResult(new
					{
						status = false,
						message = "Username is existed"
					});
				}
				_accountRepository.Add(account);

				return new JsonResult(new
				{
					status = true,
					message = "Register Success"
				});
			}
			catch(Exception ex)
			{
				return new JsonResult(new
				{
					status = false,
					message = ex.Message
				});
			}
		}
		#endregion

		#region Login
		/// <summary>
		/// Login
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("Login")]
		public IActionResult Login(string username, string password)
		{
			var account = _accountRepository.Get(x => x.Username == username && x.Password == password);
			if (account != null)
			{
				var token = GenerateToken(account);
				return new JsonResult(new
				{
					status = true,
					data = token,
					message = "Login Success"

				});
			}
			else
			{
				return new JsonResult(new
				{
					status = false,
					message = "Incorrect username or password"
				});
			}
		}
		#endregion

		#region GenerateToken
		[NonAction]
		public TokenModel GenerateToken(Account account)
		{
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, account.Id.ToString()),
				new Claim("AccountID", account.Id.ToString()),
				new Claim(ClaimTypes.Role, account.Role.ToString()),
				new Claim("Role", account.Role.ToString())
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU="));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: "YourIssuer",
				audience: "YourAudience",
				claims: claims,
				expires: DateTime.Now.AddMinutes(60),
				signingCredentials: credentials);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return new TokenModel()
			{
				Token = jwt,
				Expiration = token.ValidTo.ToLocalTime(),
			};
		}
		#endregion
	}
}
