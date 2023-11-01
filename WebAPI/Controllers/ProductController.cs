using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;



namespace WebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : Controller
	{
		private readonly ProductRepository _productRepository;
		public ProductController(ProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		#region Get All Product
		/// <summary>
		/// Get all product
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("GetAllProduct")]

		public IActionResult GetAllProduct()
		{
			ICollection<Product> products = new List<Product>();
			try
			{
				products = _productRepository.GetAll();
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
				message = "Get all product success",
				data = products
			});
		}
		#endregion

		#region Get Product By Id
		/// <summary>
		/// Get product by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetProductById/{id}")]
		public IActionResult GetProductById(Guid id)
		{
			var product = new Product();
			try
			{
				product = _productRepository.Get(x => x.Id == id);
				if (product == null)
				{
					return new JsonResult(new
					{
						status = false,
						message = "Product not found"
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
				message = "Get product by id success",
				data = product
			});
		}
		#endregion

		#region Add Product
		/// <summary>
		/// Add product
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("AddProduct")]
		public IActionResult AddProduct([FromBody]Product product)
		{
			try
			{
				_productRepository.Add(product);
				return new JsonResult(new
				{
					status = true,
					message = "Add product success"
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

		#region Update Product
		/// <summary>
		/// Update product
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		[HttpPatch]
		[Route("UpdateProduct")]
		public IActionResult UpdateProduct([FromBody]Product product)
		{
			try
			{
				_productRepository.Update(product);
				return new JsonResult(new
				{
					status = true,
					message = "Update product success"
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

		#region Delete Product
		/// <summary>
		/// Delete product
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("DeleteProduct/{id}")]
		public IActionResult DeleteProduct(Guid id)
		{
			try
			{
				_productRepository.Delete(id);
				return new JsonResult(new
				{
					status = true,
					message = "Delete product success"
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
