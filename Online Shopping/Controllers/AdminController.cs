using Online_Shopping.ServiceLayer;
using Online_Shopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        AdminService adminService = new AdminService();
        // GET: Admin
        public ActionResult PendingOrder()
        {
            List<OrderViewModel> orderViewModel = adminService.GetOrder();
            return View(orderViewModel);
        }

        [HttpPost]
        public ActionResult PendingOrder(int OrderId)
        {
            adminService.CompleteOrder(OrderId);
            return View();
        }

        public ActionResult CompleteOrder()
        {
            List<OrderViewModel> orderViewModel = adminService.GetOrder();
            return View(orderViewModel);
        }

        public ActionResult CompleteOrderDetail(int? OrderId)
        {
            if (OrderId == null)
            {
                return HttpNotFound();
            }
            List<OrderDetailViewModel> orderDetailViewModel = adminService.OrderDetail(Convert.ToInt32(OrderId));
            return View(orderDetailViewModel);
        }

        public ActionResult OrderDetail(int? OrderId)
        {
            if(OrderId == null)
            {
                return HttpNotFound();
            }
           List<OrderDetailViewModel> orderDetailViewModel = adminService.OrderDetail(Convert.ToInt32(OrderId));
            return View(orderDetailViewModel);
        }

        public JsonResult SendMailToUser(int OrderId)
        {
            bool result = false;
            string Email = adminService.GetUsername(OrderId);
            result = SendEmail(Email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SendEmail(string Email)
        {
            try
            {
                MailMessage mailMessage = new MailMessage("sriharish467764@gmail.com","sriharish467764@gmail.com");
                mailMessage.Subject = "Testing";
                mailMessage.Body = "Your OrderId 1";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "sriharish467764@gmail.com",
                    Password = "Harishsri@26"
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}