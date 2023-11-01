using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : Controller
	{
		private readonly AccountRepository _accountRepository;
		public AccountController(AccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}

		#region Get All Account
		/// <summary>
		/// Get all account
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("GetAllAccount")]
		 [Authorize(Roles = "Admin")]
		public IActionResult GetAllAccount()
		{
			ICollection<Account> accounts = new List<Account>();
			try
			{
				accounts = _accountRepository.GetAll();
			}
			catch (Exception ex)
			{
				return new JsonResult(new
				{
					status = false,
					message = ex.Message
				});
			}
			return new JsonResult(new
			{
				status = true,
				message = "Get all account success",
				data = accounts
			});
		}
		#endregion

		#region DeactivateAccount
		/// <summary>
		/// Deactivate account
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("DeactivateAccount/{id}")]
		 [Authorize(Roles = "Admin")]
		public IActionResult DeactivateAccount(Guid id)
		{
			// Check if account exist
			var account = _accountRepository.Get(x => x.Id == id);
			if (account == null)
			{
				return new JsonResult(new
				{
					status = false,
					message = "Account not found"
				});
			}
			// Check if account is active
			if (account.Status == AccountStatus.Inactive)
			{
				return new JsonResult(new
				{
					status = false,
					message = "Account is already inactive"
				});
			}
			// Deactivate account
			account.Status = AccountStatus.Inactive;
			try
			{
				_accountRepository.Update(account);
				return new JsonResult(new
				{
					status = true,
					message = "Deactivate account success"
				});

			}
			catch (Exception ex)
			{
				return new JsonResult(new
				{
					status = false,
					message = ex.Message
				});
			}

		}
		#endregion

		#region ActivateAccount
		[HttpPut]
		[Route("ActivateAccount/{id}")]
		 [Authorize(Roles = "Admin")]
		public IActionResult ActivateAccount(Guid id)
		{
			// Check if account exist
			var account = _accountRepository.Get(x => x.Id == id);
			if (account == null)
			{
				return new JsonResult(new
				{
					status = false,
					message = "Account not found"
				});
			}
			// Check if account is active
			if (account.Status == AccountStatus.Active)
			{
				return new JsonResult(new
				{
					status = false,
					message = "Account is already active"
				});
			}
			// Activate account
			account.Status = AccountStatus.Active;
			try
			{
				_accountRepository.Update(account);
				return new JsonResult(new
				{
					status = true,
					message = "Activate account success"
				});

			}
			catch (Exception ex)
			{
				return new JsonResult(new
				{
					status = false,
					message = ex.Message
				});
			}

		}
		#endregion

		#region Update Account
		/// <summary>
		/// Upadte account
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		[HttpPatch]
		[Route("UpdateAccount")]
		 [Authorize(Roles = "Admin")]
		public IActionResult UpdateAccount([FromBody] Account account)
		{
			// Check if account exist
			var accountToUpdate = _accountRepository.Get(x => x.Id == account.Id);
			if (accountToUpdate == null)
			{
				return new JsonResult(new
				{
					status = false,
					message = "Account not found"
				});
			}
			// Update account
			accountToUpdate.Username = account.Username;
			accountToUpdate.Password = account.Password;
			accountToUpdate.Role = account.Role;
			accountToUpdate.Status = account.Status;
			try
			{
				_accountRepository.Update(accountToUpdate);
				return new JsonResult(new
				{
					status = true,
					message = "Update account success"
				});

			}
			catch (Exception ex)
			{
				return new JsonResult(new
				{
					status = false,
					message = ex.Message
				});
			}

		}
		#endregion
	}
}
