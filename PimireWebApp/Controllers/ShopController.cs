using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PimireWebApp.BusinessLogicLayer;
using PimireWebApp.Models;

namespace PimireWebApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProduct _Product;
        private readonly IShop _Shop;

        public ShopController(IProduct product, IShop shop)
        {
            _Product = product;
            _Shop = shop;
        }
        [ActionName("product-list")]
        [HttpGet]
        public IActionResult ProductList()
        {
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
                return View("ProductList", subCategories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("product-detail")]
        [HttpGet]
        public IActionResult ProductDetail()
        {
            return View("ProductDetail");
        }
        [HttpGet]
        public IActionResult ProductDetail(int id)
        {
            ResponseObject responseObject;
            try
            {
                ProductSubCategory productSubCategory = _Product.GetProductSubCategory(id);

                ProductDetailsViewModel productDetailsViewModel = new ProductDetailsViewModel
                {
                    Id = id,
                    Title = productSubCategory.Title,
                    IsActive = productSubCategory.IsActive,
                    Description = productSubCategory.Description,
                    CategoryId = productSubCategory.CategoryId,
                    CategoryTitle = _Product.GetProductCategory(productSubCategory.CategoryId).Title,
                    ProductSubCategoryDetail = productSubCategory.ProductSubCategoryDetail
                };
                responseObject = new ResponseObject
                {
                    StatusCode = 200,
                    StatusMessage = "Success",
                    Data = JsonConvert.SerializeObject(productDetailsViewModel,
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
        public IActionResult ProductEnquiry(Enquiry enquiry)
        {
            if (enquiry == null)
                return BadRequest("Invalid data");

            ResponseObject responseObject;
            try
            {
                int statusCode = _Shop.ProductEnquiry(enquiry);
                responseObject = new ResponseObject
                {
                    StatusCode = statusCode,
                    StatusMessage = "Thank you for your valuable time..!"                  
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

        [ActionName("customer-enquiry")]
        [HttpGet]
        public IActionResult GetProductEnquiry()
        {
            return View("GetProductEnquiry");
        }
        [ActionName("get-customer-enquiry")]
        [HttpGet]
        public IActionResult GetEnquiry()
        {
            try
            {
                List<CustomerEnquiry> customerEnquiries = _Product.GetProductSubCategories()
                    .Join(_Shop.GetEnquiryList(), x => x.Id, y => y.SubCategoryId, (x, y) => new CustomerEnquiry
                    {
                        CategoryId = x.CategoryId,
                        SubCategoryId = y.SubCategoryId,
                        SubCategoryTitle =x.Title ,
                        CustomerAddress=y.CustomerAddress ,
                        CustomerEmail=y.CustomerEmail,
                        CustomerMobile=y.CustomerMobile,
                        CustomerName=y.CustomerName,
                        Comments=y.Comments,
                        IsEmailReceived=y.IsEmailReceived,
                        IsEmailSent=y.IsEmailSent,
                        CreatedDate=y.CreatedDate                        
                    }).ToList();
                ResponseObject responseObject = new ResponseObject
                {
                    StatusCode = 200,
                    StatusMessage = "Success",
                    Data = JsonConvert.SerializeObject(customerEnquiries)
                };
                return Json(responseObject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
