using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : Controller
	{
		private readonly OrderRepository _orderRepository;
		private readonly OrderProductRepository _orderProductRepository;
		private readonly OrderServiceRepository _orderServiceRepository;
		private readonly SswdatabaseContext _context;

		public OrderController(OrderRepository orderRepository,
		 OrderProductRepository orderProductRepository,
		  OrderServiceRepository orderServiceRepository,
		  SswdatabaseContext context
		  )
		{
			_orderRepository = orderRepository;
			_orderProductRepository = orderProductRepository;
			_orderServiceRepository = orderServiceRepository;
			_context = context;
		}

		#region Get All Order
		/// <summary>
		/// Get all product
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("GetAllOrder")]
		[Authorize]
		public IActionResult GetAllOrder()
		{
			var orderViews = new List<OrderView>();
			try
			{
				orderViews = _context.Orders
				.Select(x => new OrderView()
				{
					AccountId = x.AccountId,
					OrderId = x.Id,
					OrderDate = x.OrderDate,
					Status = x.Status,
					AccountName= _context.Accounts.Where(y=>y.Id==x.AccountId).Select(y=>y.Username).FirstOrDefault(),
					OrderProducts = _context.OderProducts.Where(y => y.OrderId == x.Id)
					.Select(y => new OrderProductView()
					{
						ProductId = y.ProductId,
						Quantity = y.Quantity,
						ProductName = _context.Products.Where(z => z.Id == y.ProductId).Select(z => z.Name).FirstOrDefault(),
					}).ToList()
					.ToList(),
				}).ToList();

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
				message = "Get all orders success",
				data = orderViews
			});
		}
		#endregion

		#region Get Order by ID
		/// <summary>
		/// Get Order by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetOrderbyID/{id}")]
		public IActionResult GetOrderByID(Guid id)
		{
			var order = new Order();
			try
			{
				order = _orderRepository.Get(x => x.Id == id);
				if (order == null)
				{
					return new JsonResult(new
					{
						status = false,
						message = "Order not found"
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
				message = "Get order by ID success",
				data = order
			});
		}
		#endregion

		#region Add Order
		/// <summary>
		/// Get all product
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("AddOrder")]
		[Authorize]
		public IActionResult AddOrder([FromBody] List<OrderRequest> items)
		{
			try
			{
				var createdOrders = new List<OrderProduct>();
				int total = 0;
				var orderId = Guid.NewGuid();
				foreach (var item in items)
				{
					createdOrders.Add(new OrderProduct
					{
						OrderId = orderId,
						ProductId = item.ProductId,
						Quantity = item.Quantity
					});
				}
				var order = new Order
				{
					Id = orderId,
					AccountId = Guid.Parse(User.Identity.Name),
					OrderDate = DateTime.Now,
					Total = items.Count,
				};
				_orderRepository.Add(order);
				_orderProductRepository.AddRange(createdOrders);

				var result = _orderRepository.SaveChanges();
				if (result == 0)
				{
					return new JsonResult(new
					{
						status = false,
						message = "Add order failed"
					});
				}
				return new JsonResult(new
				{
					status = true,
					message = "Add order success"
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

		#region Update Order
		[HttpPatch]
		[Route("UpdateOrder")]

		public IActionResult UpdateOrder([FromBody] Order order)
		{
			try
			{
				_orderRepository.Update(order);
				return new JsonResult(new
				{
					status = true,
					message = "Update order success"
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
		[HttpPut]
		[Route("ChangeStatus/{status}/{orderId}")]
		public IActionResult ChangeStatus(int status, Guid orderId)
		{
			try
			{
				var order = _orderRepository.Get(x => x.Id == orderId);
				order.Status = (OrderStatus)status;
				_orderRepository.Update(order);
				return new JsonResult(new
				{
					status = true,
					message = "Update order success"
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

		#region Delete Order
		[HttpDelete]
		[Route("DeleteOrder/{id}")]

		public IActionResult DeleteOrder(Guid id)
		{
			try
			{
				_orderRepository.Delete(id);
				return new JsonResult(new
				{
					status = true,
					message = "Delete order success"
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
