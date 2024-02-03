using PimireWebApp.Models;

namespace PimireWebApp.BusinessLogicLayer
{
    public interface IShop
    {
        int ProductEnquiry(Enquiry enquiry);
        List<Enquiry> GetEnquiryList();
    }
}
