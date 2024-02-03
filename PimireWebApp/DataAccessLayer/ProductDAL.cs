using Microsoft.EntityFrameworkCore;
using PimireWebApp.BusinessLogicLayer;
using PimireWebApp.Models;

namespace PimireWebApp.DataAccessLayer
{
    public class ProductDAL : IProduct
    {
        private readonly PimireDbContext _pimireDbContext;
        public ProductDAL(PimireDbContext pimireDbContext)
        {
            _pimireDbContext = pimireDbContext;
        }
        public int AddProductCategory(ProductCategory productCategory)
        {
            try
            {
                if (_pimireDbContext.ProductCategories.Any(x => x.Title == productCategory.Title))
                    return -302;
                _pimireDbContext.ProductCategories.Add(productCategory);
                _pimireDbContext.SaveChanges();
                return productCategory.Id > 0 ? 200 : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int AddProductSubCategory(ProductSubCategory subCategory)
        {
            try
            {
                if (_pimireDbContext.ProductSubCategories.Any(x => x.Title == subCategory.Title))
                    return -302;
                _pimireDbContext.ProductSubCategories.Add(subCategory);
                _pimireDbContext.SaveChanges();
                return subCategory.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int DeleteProductCategory(int id)
        {
            try
            {
                int statusCode = 0;
                ProductCategory productCategory = _pimireDbContext.ProductCategories.Where(x => x.Id == id).FirstOrDefault();
                if (productCategory == null)
                    return -302;
               var entity = _pimireDbContext.ProductCategories.Remove(productCategory);
                _pimireDbContext.SaveChanges();
                if (entity.State == EntityState.Detached)
                    statusCode = 200;
                return statusCode;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ProductCategory> GetProductCategories()
        {
            try
            {
                return _pimireDbContext.ProductCategories.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ProductCategory GetProductCategory(int id)
        {
            return _pimireDbContext.ProductCategories.Where(x=>x.Id == id).FirstOrDefault();
        }

        public ProductSubCategory GetProductSubCategory(int id)
        {
            try
            {
                var productSubCategory = _pimireDbContext.ProductSubCategories.Where(x => x.Id == id).FirstOrDefault();
                if(productSubCategory != null)
                {
                    productSubCategory.ProductSubCategoryDetail = _pimireDbContext.ProductSubCategoryDetails.Where(x => x.SubCategoryId == id).FirstOrDefault();
                }
                return productSubCategory;
            }
            catch
            {
                throw;
            }
        }

        public List<ProductSubCategory> GetProductSubCategories()
        {
            try
            {
                return _pimireDbContext.ProductSubCategories.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProductSubCategoryDetail> GetProductSubCategoriesDetail()
        {
            try
            {
                return _pimireDbContext.ProductSubCategoryDetails.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int UpdateProductCategory(ProductCategory productCategory)
        {
            try
            {
                if (_pimireDbContext.ProductCategories.Any(x => x.Id != productCategory.Id && x.Title == productCategory.Title))
                    return -302;
                _pimireDbContext.ProductCategories.Update(productCategory);
                _pimireDbContext.SaveChanges();
                return productCategory.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int UpdateProductSubCategory(ProductSubCategory productSubCategory)
        {
            try
            {
                if (_pimireDbContext.ProductCategories.Any(x => x.Id != productSubCategory.Id && x.Title == productSubCategory.Title))
                    return -302;
                ProductSubCategoryDetail productSubCategoryDetail = _pimireDbContext.ProductSubCategoryDetails.First(x => x.SubCategoryId == productSubCategory.Id);

                if (!String.IsNullOrEmpty(productSubCategory.ProductSubCategoryDetail.ImageUrl))
                    productSubCategoryDetail.ImageUrl = productSubCategory.ProductSubCategoryDetail.ImageUrl;
                productSubCategoryDetail.Price = productSubCategory.ProductSubCategoryDetail.Price;
                productSubCategoryDetail.MfgDate = productSubCategory.ProductSubCategoryDetail.MfgDate;
                productSubCategory.ProductSubCategoryDetail = productSubCategoryDetail;
                _pimireDbContext.ProductSubCategories.Update(productSubCategory);
                _pimireDbContext.SaveChanges();
                return productSubCategory.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int DeleteProductSubCategory(int id)
        {
            try
            {
                int statusCode = 0;
                ProductSubCategory productSubCategory = _pimireDbContext.ProductSubCategories.Where(x => x.Id == id).FirstOrDefault();
                ProductSubCategoryDetail productSubCategoryDetail = _pimireDbContext.ProductSubCategoryDetails.Where(x => x.SubCategoryId == id).FirstOrDefault();
                if (productSubCategory == null)
                    return -302;

                _pimireDbContext.ProductSubCategoryDetails.Remove(productSubCategoryDetail);
                var entity = _pimireDbContext.ProductSubCategories.Remove(productSubCategory);

                _pimireDbContext.SaveChanges();
                if (entity.State == EntityState.Detached)
                    statusCode = 200;
                return statusCode;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
