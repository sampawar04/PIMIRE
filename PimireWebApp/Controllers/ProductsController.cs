using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PimireWebApp.BusinessLogicLayer;
using PimireWebApp.Models;
using PimireWebApp.Utilities;

namespace PimireWebApp.Controllers
{
    [SessionTimeout]
    public class ProductsController : Controller
    {
        private readonly IProduct _Product;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public ProductsController(IProduct product, IWebHostEnvironment webHostEnvironment)
        {
            _Product = product;
            _WebHostEnvironment = webHostEnvironment;
        }

        #region Product Category

        [HttpGet]
        public IActionResult GetProductCategories()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProductCategories()
        {
            ResponseObject responseObject;
            try
            {
                List<ProductCategory> categories = _Product.GetProductCategories();

                responseObject = new ResponseObject
                {
                    StatusCode = 200,
                    StatusMessage = "Success",
                    Data = JsonConvert.SerializeObject(categories)
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        [HttpGet]
        public IActionResult AddProductCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProductCategory(ProductCategory productCategory)
        {
            ResponseObject responseObject;
            try
            {
                if (productCategory == null)
                {
                    return BadRequest("Invalid Data");
                }

                int productCategoryId = _Product.AddProductCategory(productCategory);

                responseObject = new ResponseObject
                {
                    StatusCode = productCategoryId,
                    StatusMessage = productCategoryId > 0 ? "Data saved successfully!" : "Error in saving data"
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        [HttpGet]
        public IActionResult EditProductCategory()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetProductCategory(int id)
        {
            ResponseObject responseObject;
            try
            {
                ProductCategory productCategory = _Product.GetProductCategory(id);

                responseObject = new ResponseObject
                {
                    StatusCode = 200,
                    StatusMessage = "Success",
                    Data = JsonConvert.SerializeObject(productCategory)
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        [HttpPost]
        public IActionResult UpdateProductCategory(ProductCategory productCategory)
        {
            ResponseObject responseObject;
            try
            {
                if (productCategory == null || productCategory.Id == 0)
                {
                    return BadRequest("Invalid Data");
                }

                int productCategoryId = _Product.UpdateProductCategory(productCategory);

                responseObject = new ResponseObject
                {
                    StatusCode = productCategoryId,
                    StatusMessage = productCategoryId > 0 ? "Data saved successfully!" : "Error in saving data"
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        [HttpGet]
        public IActionResult DeleteProductCategory(int id)
        {
            ResponseObject responseObject;
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid Data");
                }

                int statusCode = _Product.DeleteProductCategory(id);

                responseObject = new ResponseObject
                {
                    StatusCode = statusCode,
                    StatusMessage = statusCode > 0 ? "Data deleted successfully!" : (statusCode == -302 ? "Product Category is not found" : "Error in deletion")
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        #endregion

        #region Product Sub Category
        [HttpGet]
        public IActionResult GetProductSubCategories()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProductSubCategories()
        {
            ResponseObject responseObject;
            try
            {
                List<SubCategoryViewModel> subCategories = _Product.GetProductSubCategories()
                    .Join(_Product.GetProductCategories(), x => x.CategoryId, y => y.Id, (x, y) => new SubCategoryViewModel
                    {
                        CategoryId = x.CategoryId,
                        CategoryTitle = y.Title,
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                .Join(_Product.GetProductSubCategoriesDetail(), x => x.Id, y => y.SubCategoryId, (x, y) => new SubCategoryViewModel
                {
                    CategoryId = x.CategoryId,
                    CategoryTitle = x.CategoryTitle,
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    ImageURL = y.ImageUrl
                }).ToList();

                responseObject = new ResponseObject
                {
                    StatusCode = 200,
                    StatusMessage = "Success",
                    Data = JsonConvert.SerializeObject(subCategories)
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        [HttpGet]
        public IActionResult EditProductSubCategory()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetProductSubCategory(int id)
        {
            ResponseObject responseObject;
            try
            {
                ProductSubCategory productSubCategory = _Product.GetProductSubCategory(id);

                responseObject = new ResponseObject
                {
                    StatusCode = 200,
                    StatusMessage = "Success",
                    Data = JsonConvert.SerializeObject(productSubCategory, 
                    Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        }
                        )
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        [HttpPost]
        public IActionResult UpdateProductSubCategory(string subCategoryJSON)
        {
            ResponseObject responseObject;
            try
            {
                SubCategoryViewModel subCategoryViewModel = JsonConvert.DeserializeObject<SubCategoryViewModel>(subCategoryJSON);
                subCategoryViewModel.SubCategoryDetailViewModel.Files = HttpContext.Request.Form.Files.ToList();

                if (subCategoryViewModel == null)
                    return BadRequest("Error in data");
                List<string> imageURLs = new List<string>();
                foreach (IFormFile file in subCategoryViewModel?.SubCategoryDetailViewModel?.Files ?? Enumerable.Empty<IFormFile>())
                {
                    if(file.Length > 0)
                    {
                        string imgURL = SaveImageFile(file);
                        imageURLs.Add(imgURL);
                    }                    
                }
                ProductSubCategory subCategory = new ProductSubCategory
                {
                    Id = subCategoryViewModel.Id,
                    CategoryId = subCategoryViewModel.CategoryId,
                    Description = subCategoryViewModel.Description,
                    IsActive = subCategoryViewModel.IsActive,
                    Title = subCategoryViewModel.Title,
                    ProductSubCategoryDetail = new ProductSubCategoryDetail
                    {
                        MfgDate = subCategoryViewModel.SubCategoryDetailViewModel.MfgDate,
                        Price = subCategoryViewModel.SubCategoryDetailViewModel.Price,
                        ImageUrl = string.Join(";", imageURLs)
                    }
                };
                int categoryId = _Product.UpdateProductSubCategory(subCategory);

                responseObject = new ResponseObject
                {
                    StatusCode = categoryId,
                    StatusMessage = categoryId > 0 ? "Data saved successfully!" : "Error in saving data"
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        [HttpGet]
        public IActionResult DeleteProductSubCategory(int id)
        {
            ResponseObject responseObject;
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid Data");
                }

                int statusCode = _Product.DeleteProductSubCategory(id);

                responseObject = new ResponseObject
                {
                    StatusCode = statusCode,
                    StatusMessage = statusCode > 0 ? "Data deleted successfully!" : (statusCode == -302 ? "Product Category is not found" : "Error in deletion")
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }

        [ActionName("add-product")]
        [HttpGet]
        public IActionResult AddProductSubCategory()
        {
            return View("AddProductSubCategory");
        }
        [HttpPost]
        public IActionResult AddProductSubCategory(string subCategoryJSON)
        {
            ResponseObject responseObject;
            try
            {
                SubCategoryViewModel subCategoryViewModel = JsonConvert.DeserializeObject<SubCategoryViewModel>(subCategoryJSON);
                subCategoryViewModel.SubCategoryDetailViewModel.Files = HttpContext.Request.Form.Files.ToList();
                
                if (subCategoryViewModel == null || subCategoryViewModel.SubCategoryDetailViewModel.Files == null)
                    return BadRequest("Error in data");

                if (subCategoryViewModel.SubCategoryDetailViewModel?.Files?.Count == 0)
                    return BadRequest("Please upload a valid file");

                List<string> imageURLs = new List<string>();
                foreach (IFormFile file in subCategoryViewModel.SubCategoryDetailViewModel.Files)
                {
                  string imgURL = SaveImageFile(file);
                    imageURLs.Add(imgURL);
                }
                ProductSubCategory subCategory = new ProductSubCategory
                {
                    Id = subCategoryViewModel.Id,
                    CategoryId = subCategoryViewModel.CategoryId,
                    Description = subCategoryViewModel.Description,
                    IsActive = true,
                    Title = subCategoryViewModel.Title,
                    ProductSubCategoryDetail = new ProductSubCategoryDetail
                    {
                        MfgDate = subCategoryViewModel.SubCategoryDetailViewModel.MfgDate,
                        Price = subCategoryViewModel.SubCategoryDetailViewModel.Price,
                        ImageUrl = string.Join(";", imageURLs)
                    }
                };
                int categoryId = _Product.AddProductSubCategory(subCategory);

                responseObject = new ResponseObject
                {
                    StatusCode = categoryId,
                    StatusMessage = categoryId > 0 ? "Data saved successfully!" : "Error in saving data"
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }


        [HttpGet]
        public IActionResult GetProductCategoriesDropdown()
        {
            ResponseObject responseObject;
            try
            {
                var selectListItems = _Product.GetProductCategories().Where(x=>x.IsActive).Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

                responseObject = new ResponseObject
                {
                    StatusCode = 200,
                    StatusMessage = "Success",
                    Data = JsonConvert.SerializeObject(selectListItems)
                };
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    StatusCode = -500,
                    StatusMessage = "Something went wrong, Please contact to administrator!",
                    Data = ex.Message
                };
            }
            return Json(responseObject);
        }
        private string GetUniqueValue()
        {
            var ticks = new DateTime(2016, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var uniqueId = ans.ToString("x");
            return uniqueId;
        }
        private string SaveImageFile(IFormFile file)
        {
            string folderPath = Path.Combine("images", "products");
            string folderFilePath = Path.Combine(folderPath, GetUniqueValue() + "_" + file.FileName);
            string filePath = Path.Combine(_WebHostEnvironment.WebRootPath, folderFilePath);
            if (file.Length > 0)
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);                    
                }
            }
            return folderFilePath;
        }
        #endregion
    }
}
