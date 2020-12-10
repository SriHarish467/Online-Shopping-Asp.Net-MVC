using Online_Shopping.ServiceLayer;
using Online_Shopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Online_Shopping.Controllers
{
    public class ManageCartController : Controller
    {
        ManageCartService manageCartService = new ManageCartService();
        

        public ActionResult AddCart(int? ProductId, string ProductName, decimal Price, string Description)
        {
            if (ProductId == null)
            {
                return HttpNotFound();
            }
            List<CartViewModel> item = new List<CartViewModel>();
            if (Session["Username"] == null)
            {
                if (Session["Cart"] == null)
                {
                    item = manageCartService.AddCart
                    (Convert.ToInt32(ProductId), item, Price, ProductName, Description);
                    Session["Cart"] = item;
                }
                else
                {
                    item = Session["Cart"] as List<CartViewModel>;
                    item = manageCartService.AddCart
                       (Convert.ToInt32(ProductId), item, Price, ProductName, Description);
                    Session["Cart"] = item;
                }
                Session["Count"] = item.Count;
            }
            else
            {
                Session["Count"] = manageCartService.CartDb(Convert.ToInt32(ProductId), Convert.ToString(Session["Username"]));

            }

            return RedirectToAction("DisplayProduct","Product");
        }

        public ActionResult DisplayCart()
        {
            
            if (Session["Username"] == null)
            {
                List<CartViewModel> cart = (List<CartViewModel>)Session["Cart"];
                return View(cart);
            }
            else
            {
                List<CartViewModel> cartViewModel = manageCartService.DisplayCart(Convert.ToString(Session["Username"]));
                return View(cartViewModel);
            }
        }

        public ActionResult DeleteCartProduct(int? ProductId)
        {
            if (ProductId == null)
            {
                return HttpNotFound();
            }
            if (Session["Username"] == null)
            {
                Session["Count"] = manageCartService.DeleteCartProduct((List<CartViewModel>) Session["Cart"],Convert.ToInt32(ProductId));
                
            }
            else
            {
                Session["Count"] = manageCartService.DeleteCartProduct(Convert.ToInt32(ProductId), Convert.ToString(Session["Username"]));
            }
            return RedirectToAction("DisplayCart");
        }

        public ActionResult IncreaseQuantity(int ProductId)
        {
            if(Session["Username"] != null)
            {
                manageCartService.IncreaseQuantity(ProductId,Convert.ToString(Session["Username"]));
            }
            else
            {
                manageCartService.IncreaseQuantity((List<CartViewModel>)Session["Cart"],ProductId);
            }
            return RedirectToAction("DisplayCart");
        }

        public ActionResult DecreaseQuantity(int ProductId)
        {
            if (Session["Username"] != null)
            {
                manageCartService.DecreaseQuantity(ProductId, Convert.ToString(Session["Username"]));
            }
            else
            {
                manageCartService.DecreaseQuantity((List<CartViewModel>) Session["Cart"],ProductId);
            }
            
            return RedirectToAction("DisplayCart");
        }

        [Authorize]
        public ActionResult Checkout()
        {
            List<CartViewModel> cartViewModel = manageCartService.DisplayCart(Convert.ToString(Session["Username"]));
            Session["Checkout"] = 1;
            return View(cartViewModel);
        }

       [Authorize]
        public ActionResult ProductOrder()
        {
            if (Convert.ToInt32(Session["Checkout"]) == 1)
            {
                manageCartService.Checkout(Convert.ToString(Session["Username"]));
                manageCartService.RemoveCart(Convert.ToString(Session["Username"]));
                Session["Count"] = null;
            }
            else
            {
                return Redirect("DisplayCart");
            }
            return View();
        }

        public ActionResult YourOrder()
        {

        }
    }

}
