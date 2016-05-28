using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aghaApi.Models;
using System.Net.Mail;
using System.Web.UI;

namespace aghaApi.Controllers
{
    public class HomeController : Controller
    {
        finalEntities ctx = new finalEntities();
        public ActionResult Index()
        {
            return View();
        }

        public Object aboutUs()
        {
            return View();
        }


        public Object sendMail(String from, String name, String subject, String message)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Credentials = new System.Net.NetworkCredential("hussnaindar17@gmail.com", "53zircon");
            smtpServer.Port = 587; // Gmail works on this port
            smtpServer.EnableSsl = true;
            mail.From = new MailAddress(from);
            mail.To.Add("hussnaindar17@yahoo.com");
            mail.Subject = subject;
            mail.Body = message + "\n" + from;

            smtpServer.Send(mail);

            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public ActionResult signUp2(String name, String cnic, String address, String username, String contact)
        {
            user_request ur = new user_request();
            ur.name = name;
            ur.cnic = Convert.ToDecimal(cnic.Replace("-", ""));
            ur.address = address;
            ur.username = username;
            ur.contact = Convert.ToDecimal(contact.Replace("-", ""));

            try
            {
                finalEntities dben = new finalEntities();

                foreach (user u in dben.users)
                {
                    if (u.username.Equals(ur.username))
                    {
                        TempData["error"] = "Error";
                        return RedirectToAction("Signup", "Home");

                    }
                }

                dben.user_request.Add(ur);
                dben.SaveChanges();

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    file.SaveAs(Server.MapPath(@"~\images\user_request\user_" + ur.Id + ".jpeg"));
                    break;
                }
                TempData["error"] = "Error";
                Response.Write(@"<script language='javascript'>alert('The following errors have occurred:  .');</script>");
                // Response.Write("<script>alert('Data inserted successfully')</script>");
            }
            catch (Exception e)
            {
                Exception e2 = e.InnerException;
            }
            return RedirectToAction("Index", "Home");


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult AllServices()
        {
            List<service> list = ctx.services.ToList();
            return View(list);
        }
    }
}