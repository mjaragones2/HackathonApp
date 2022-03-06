using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HackathonApp.Models;
using Microsoft.AspNet.Identity;
using PayPal.Api;

namespace HackathonApp.Controllers
{
    public class PaypalController : Controller
    {
        // GET: Paypal
        public ActionResult PaymentWithPaypal(decimal? AmountGiven, int? Fundid, string Message, string Cancel = null)
        {
            //getting the apiContext  
            Session["AmountGiven"] = AmountGiven;
            Session["Fundid"] = Fundid;
            Session["Message"] = Message;
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.
            //
            decimal amount = decimal.Parse(Session["AmountGiven"].ToString());
            int fundid = int.Parse(Session["Fundid"].ToString());
            string message = Session["Message"].ToString();
            var userid = User.Identity.GetUserId();
            var db = new ApplicationDbContext();
            var getfunddetail = db.Funds.Where(x => x.Id == Fundid).FirstOrDefault();
            getfunddetail.AmountAcquired += amount;
            db.Entry(getfunddetail).State = EntityState.Modified;
            db.SaveChanges();

            var fundt = new FundTransaction
            {
                AmountGiven = amount,
                DateCreated = DateTime.Now,
                Fundid = fundid,
                Userid = userid,
                Message = message
            };
            db.FundTransactions.Add(fundt);
            db.SaveChanges();

            var checkwallet = db.Wallet.Where(x => x.Userid == userid).FirstOrDefault();
            if (checkwallet == null)
            {
                var wallet = new EWallet
                {
                    Balance = amount,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    Userid = userid
                };
                db.Wallet.Add(wallet);
                db.SaveChanges();
            }
            else
            {
                checkwallet.Balance += amount;
                checkwallet.DateUpdated = DateTime.Now;
                db.Entry(checkwallet).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            Session["AmountGiven"] = Session["AmountGiven"].ToString();
            Session["Fundid"] = Session["Fundid"].ToString();
            Session["Message"] = Session["Message"].ToString();
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            Session["AmountGiven"] = Session["AmountGiven"].ToString();
            Session["Fundid"] = Session["Fundid"].ToString();
            Session["Message"] = Session["Message"].ToString();
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Fund",
                currency = "PHP",
                price = ""+ Session["AmountGiven"].ToString(),
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = "" + Session["AmountGiven"].ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "PHP",
                total = "" + Session["AmountGiven"].ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Fundraising",
                invoice_number = Convert.ToString((new Random()).Next(100000)), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}