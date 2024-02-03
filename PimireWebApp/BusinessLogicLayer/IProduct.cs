using PimireWebApp.Models;

namespace PimireWebApp.BusinessLogicLayer
{
    public interface IProduct
    {        
        int AddProductCategory(ProductCategory productCategory);        
        List<ProductCategory> GetProductCategories();
        ProductCategory GetProductCategory(int id);
        int UpdateProductCategory(ProductCategory productCategory);
        int DeleteProductCategory(int id);
        List<ProductSubCategory> GetProductSubCategories();
        List<ProductSubCategoryDetail> GetProductSubCategoriesDetail();
        int AddProductSubCategory(ProductSubCategory subCategory);
        ProductSubCategory GetProductSubCategory(int id);
        int UpdateProductSubCategory(ProductSubCategory productSubCategory);
        int DeleteProductSubCategory(int id);
    }
}
