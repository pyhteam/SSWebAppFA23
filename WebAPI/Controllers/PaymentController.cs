using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace WebAPI.Controllers
{
    public class PaymentController: Controller
    {
        private readonly PaymentRepository _paymentRepository;
        public PaymentController(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        #region Get All Payment
        /// <summary>
        /// Get all payment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllPayment")]

        public IActionResult GetAllPayment() 
        {
            ICollection<Payment> payment = new List<Payment>();
            try
            {
                payment = _paymentRepository.GetAll();

            }catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = false,
                    message = ex.Message 
                }) ;
            }
            return new JsonResult(new
            {
                status = true,
                message = "Get All payment",
                data = payment
            }) ;
        }
        #endregion

        #region Get Payment by Id
        /// <summary>
        /// Get Payment by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaymentbyId")]
        public IActionResult GetPaymentbyId(Guid id) 
        {
            var payment = new Payment();           
            try
            {
                payment = _paymentRepository.Get(x => x.Id == id);
                if (payment == null)
                {
                    return new JsonResult(new
                    {
                        status = false,
                        message = "Payment not found"
                    });
                }
            }
            catch (Exception ex) 
            {
                    return new JsonResult(new
                    { 
                    status = false,
                    Message = ex.Message
                    });
            }
            return new JsonResult(new 
            { 
                status = true,
                message = "Get Payment by Id",
                data = payment
            });
        }
        #endregion

        #region Add Payment
        /// <summary>
        /// Add Payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPayment")]

        public IActionResult AddPayment([FromBody] Payment payment) 
        {
            try
            {
                _paymentRepository.Add(payment);
                return new JsonResult(new 
                { 
                status = true,
                message = "Add payment success"
                });
            }catch (Exception ex) 
            {
                return new JsonResult(new
                {
                    status = false,
                    message = ex.Message
                });
            
            }
        }
        #endregion

        #region Update Payment
        /// <summary>
        /// Update Payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePayment")]
        public IActionResult UpdatePayment([FromBody] Payment payment)
        {
            try 
            {
                _paymentRepository.Update(payment);
                return new JsonResult(new
                {
                    status = true,
                    message = "Update Payment succes"
                });
            }catch (Exception ex) 
            {
                return new JsonResult(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region Delete Payment
        /// <summary>
        /// Delete Payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeletePayment/{id}")]

        public IActionResult DeletePayment(Guid id)
        {
            try
            {
                _paymentRepository.Delete(id);
                return new JsonResult(new
                {
                    status = true,
                    message = "Delete product success"
                });
            }catch(Exception ex) 
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
