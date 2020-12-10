using Online_Shopping.ServiceLayer;
using Online_Shopping.ViewModel;
using System;
using System.Collections.Generic;
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
        public ActionResult CompleteOrder()
        {
            List<OrderViewModel> orderViewModel = adminService.GetOrder();
            return View(orderViewModel);
        }

        [HttpPost]
        public ActionResult CompleteOrder(int OrderId)
        {
            adminService.CompleteOrder(OrderId);
            return View();
        }
        public ActionResult CancelOrder()
        {
            List<OrderViewModel> orderViewModel = adminService.GetOrder();
            return View(orderViewModel);
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
    }
}