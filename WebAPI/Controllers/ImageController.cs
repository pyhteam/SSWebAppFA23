using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly ImageRepository _imageRepository;
        public ImageController(ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        #region Get All Images
        /// <summary>
        /// Get All Image
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllImage")]
        public IActionResult GetAllImage()
        {
            ICollection<Image> images = new List<Image>();
            try
            {
                images = _imageRepository.GetAll();
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
                message = "Get all image success",
                data = images
            });
        }
        #endregion

        #region Get Image by ID
        /// <summary>
        /// Get Image by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetImageById/{id}")]
        public IActionResult GetImageById(Guid id)
        {
            var image = new Image();
            try
            {
                image = _imageRepository.Get(x => x.Id == id);
                if (image == null)
                {
                    return new JsonResult(new
                    {
                        status = false,
                        message = "Image not found"
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
                message= "Get Product by id success"
            });
        }
        #endregion

        #region Get Image by AccountID
        /// <summary>
        /// Get Image By AccountId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetImageByAccountId/{id}")]
        public IActionResult GetImageByAccountId(Guid id)
        {
            var image = new Image();
            try
            {
                image = _imageRepository.Get(x => x.AccountId == id);
                if (image == null)
                {
                    return new JsonResult(new
                    {
                        status = false,
                        message = "Image not found"
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
                message = "Get Image by AccountID success"
            });
        }
        #endregion

        #region Add Image
        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddImage")]
        public IActionResult AddImage([FromBody] Image image)
        {
            try
            {
                _imageRepository.Add(image);
                return new JsonResult(new
                {
                    status = true,
                    message = "Add Image success"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = true,
                    message = "Add Image success"
                });
            }
        }
        #endregion

        #region Update Image
        /// <summary>
        /// Update Image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateImage")]
        public IActionResult UpdateImage([FromBody] Image image)
        {
            try
            {
                _imageRepository.Update(image);
                return new JsonResult(new
                {
                    status = true,
                    message = "Update image success"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status= false,
                    message= ex.Message
                });
            }
        }
        #endregion

        #region Delete Image
        /// <summary>
        /// Delete Image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteImage/{id}")]
        public IActionResult DeleteImage(Guid id)
        {
            try
            {
                _imageRepository.Delete(id);
                return new JsonResult(new
                {
                    status = true,
                    message = "Delete image success"
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
