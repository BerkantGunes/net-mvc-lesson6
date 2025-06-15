using CAR.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CAR.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Index(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fromAddress = new MailAddress("ugurertas99@gmail.com", "Your App");
                    var toAddress = new MailAddress("ugurertas99@gmail.com"," Ugur Ertas");
                    const string fromPassword = "mpoe kinr lhba ljgt";
                    string subject = "New Contact Message";
                    string body = $"Name: {model.Name}\nEmail:{model.Email}\nMessage:{model.Message}";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                        Timeout = 20000,
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body

                    })
                    {
                        smtp.Send(message);

                    }
                    ViewBag.Message = "Information sent succesfully!";
                }
                catch
                {
                    ViewBag.Message = "Failed to send email.";

                }

               
            }
            return View(model);
        }


    }
}
