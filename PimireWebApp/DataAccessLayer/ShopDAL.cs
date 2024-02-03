using PimireWebApp.BusinessLogicLayer;
using PimireWebApp.Models;
using PimireWebApp.Utilities;

namespace PimireWebApp.DataAccessLayer
{
    public class ShopDAL : IShop
    {
        private readonly PimireDbContext _pimireDbContext;
        private readonly IEmailHelper _emailHelper;
        public ShopDAL(PimireDbContext pimireDbContext, IEmailHelper emailHelper)
        {
            _pimireDbContext = pimireDbContext;
            _emailHelper = emailHelper;
        }

        public List<Enquiry> GetEnquiryList()
        {
            return _pimireDbContext.Enquiries.ToList();            
        }

        public int ProductEnquiry(Enquiry enquiry)
        {
            try
            {
                
                string productName = _pimireDbContext.ProductSubCategories.Where(x => x.Id == enquiry.SubCategoryId).First().Title;
                // Sending email to customer 
                bool isEmailSent = _emailHelper.Send(enquiry.CustomerEmail, "Product Enquiry For " + productName, "Thank you for showing interest in our products.");
                string emailBody = "You have received a an enquiry for the product " + productName + ".\n Customer Name: " + enquiry.CustomerName + "\n Customer Mobile: " + enquiry.CustomerMobile + "\n Customer Email: " + enquiry.CustomerEmail + "\n Customer Comments: " + enquiry.Comments;
                
                enquiry.IsEmailSent = isEmailSent;
                _pimireDbContext.Enquiries.Add(enquiry);
                // Sending email to owner
                bool isEmailReceived = _emailHelper.Send(enquiry.CustomerEmail, "Product Enquiry For " + productName, emailBody);
                enquiry.IsEmailReceived = isEmailReceived;
                enquiry.CreatedDate = DateTime.Now;
                _pimireDbContext.SaveChanges();
                return enquiry.Id > 0 ? 200 : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
