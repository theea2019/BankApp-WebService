using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Bank.BusinessLogic;
using Bank.Models.Concretes;

namespace BankApp_WebService
{
    /// <summary>
    /// Summary description for BankAppWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BankAppWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool InsertCustomer(Customers entity)
        {
            try
            {
                using (var business = new CustomersBusiness())
                {
                    business.InsertCustomer(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool UpdateCustomer (Customers entity)
        {
            try
            {
                using (var business = new CustomersBusiness())
                {
                    business.UpdateCustomer(entity);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public bool DeleteCustomer (int id)
        {
            try
            {
                using (var business = new CustomersBusiness())
                {
                    business.DeleteCustomerById(id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public List<Customers> SelectAllCustomers()
        {
            try
            {
                using (var business = new CustomersBusiness())
                {
                    return business.SelectAllCustomers();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public Customers SelectCustomerById(int id)
        {
            try
            {
                using (var business = new CustomersBusiness())
                {
                    return business.SelectCustomerById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
