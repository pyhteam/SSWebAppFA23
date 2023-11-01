using Microsoft.AspNetCore.Mvc;
using DataLayer.Entities;
using DataLayer.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AccountDetailController : Controller
    {
        private readonly AccountDetailRepository _accountDetailRepository;
        public AccountDetailController(AccountDetailRepository accountDetailRepository)
        {
            _accountDetailRepository = accountDetailRepository;
        }

        #region Get All Acc Detail
        /// <summary>
        /// Get all detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllAccountDetail")]

        public IActionResult GetAllaccountDetail()
        {
            ICollection<AccountDetail> accountDetails = new List<AccountDetail>();
            try
            {
                accountDetails = _accountDetailRepository.GetAll();
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
                message = "Get all Account Detail success",
                data = accountDetails
            });
        }
        #endregion

        #region Get accountDetail By Id
        /// <summary>
        /// Get accountDetail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetaccountDetailById/{id}")]
        public IActionResult GetAccountDetailById(Guid id)
        {
            var accountDetail = new AccountDetail();
            try
            {
                accountDetail = _accountDetailRepository.Get(x => x.Id == id);
                if (accountDetail == null)
                {
                    return new JsonResult(new
                    {
                        status = false,
                        message = "Account not found"
                    });
                }
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
                message = "Get Account Detail by id success",
                data = accountDetail
            });
        }
        #endregion

        #region Add Account Detail
        /// <summary>
        /// Add accountDetail
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddAccountDetail")]
        public IActionResult AddAccountDetail([FromBody] AccountDetail accountDetail)
        {
            try
            {
                _accountDetailRepository.Add(accountDetail);
                return new JsonResult(new
                {
                    status = true,
                    message = "Add Account Detail success"
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

        #region Update Account Detail
        /// <summary>	
        /// Update accountDetail
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("UpdateaccountDetail")]
        public IActionResult UpdateAccountDetail([FromBody] AccountDetail accountDetail)
        {
            try
            {
                _accountDetailRepository.Update(accountDetail);
                return new JsonResult(new
                {
                    status = true,
                    message = "Update accountDetail success"
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

        #region Delete Account Detail
        /// <summary>
        /// Delete accountDetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteAccountDetail/{id}")]
        public IActionResult DeleteAccountDetail(Guid id)
        {
            try
            {
                _accountDetailRepository.Delete(id);
                return new JsonResult(new
                {
                    status = true,
                    message = "Delete Account Detail success"
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
