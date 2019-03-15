using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Bank.BusinessLogic;
using Bank.Commons.Concretes.Helpers;
using Bank.Commons.Concretes.Logger;
using Bank.Models.Concretes;

namespace BankApp_WebService
{
    /// <summary>
    /// Summary description for TransactionWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TransactionWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool Deposit(int senderId, decimal amount)
        {
            try
            {
                using (var transactionBussiness = new TransactionBusiness())
                {
                    Transactions transaction = new Transactions()
                    {
                        TransactorAccountNumber = senderId,
                        ReceiverAccountNumber = senderId,
                        TransactionAmount = amount,
                        TransactionDate = DateTime.Now,
                        isSuccess = false
                    };

                    using (var customerBussiness = new CustomersBusiness())
                    {
                        return transactionBussiness.DepositMoney(transaction, customerBussiness.SelectCustomerById(senderId));
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Log(LogTarget.File,
                    "Deposit failed: " + senderId + "." + "\n" + ExceptionHelper.ExceptionToString(e));
                return false;
            }
        }

        [WebMethod]
        public bool Withdraw(int senderId, decimal amount)
        {
            try
            {
                using (var transactionBussiness = new TransactionBusiness())
                {
                    Transactions transaction = new Transactions()
                    {
                        TransactorAccountNumber = senderId,
                        ReceiverAccountNumber = null,
                        TransactionAmount = amount,
                        TransactionDate = DateTime.Now,
                        isSuccess = false
                    };

                    using (var customerBussiness = new CustomersBusiness())
                    {
                        return transactionBussiness.WithdrawMoney(transaction, customerBussiness.SelectCustomerById(senderId));
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Log(LogTarget.File, "Withdraw failed: " + senderId + "." + "\n" + ExceptionHelper.ExceptionToString(e));
                return false;
            }
        }

        [WebMethod]
        public bool Transfer(int senderId, int receiverId, decimal amount)
        {
            try
            {
                using (var transactionBussiness = new TransactionBusiness())
                {
                    Transactions transaction = new Transactions()
                    {
                        TransactorAccountNumber = senderId,
                        ReceiverAccountNumber = receiverId,
                        TransactionAmount = amount,
                        TransactionDate = DateTime.Now,
                        isSuccess = false
                    };
                    using (var customerBussiness = new CustomersBusiness())
                    {
                        return transactionBussiness.MakeTransaction(transaction, customerBussiness.SelectCustomerById(senderId),customerBussiness.SelectCustomerById(receiverId));
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Log(LogTarget.File,"Transfer failed betweeen: "+ senderId + " and "+ receiverId + "." + "\n" + ExceptionHelper.ExceptionToString(e));
                return false;
            }
        }
    }
}
