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
            return View(cartViewModel);
        }

       [Authorize]
        public ActionResult CartPlaceOrder()
        {
            
            bool value = manageCartService.CheckDetail(Convert.ToString(Session["Username"]));
            if (value)
            {
                return Redirect("CartShippingDetail");
            }
            else
            {
                manageCartService.Checkout(Convert.ToString(Session["Username"]));
                manageCartService.RemoveCart(Convert.ToString(Session["Username"]));
                Session["Count"] = null;
                return RedirectToAction("DisplayProduct", "Product");
            }
        }

        public ActionResult CartShippingDetail()
        {
            UserDetailViewModel userDetailViewModel = manageCartService.ShippingDetail(Convert.ToString(Session["Username"]));
            return View(userDetailViewModel);
        }

        [HttpPost]
        public ActionResult CartShippingDetail(UserDetailViewModel userDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                manageCartService.ShippingDetail(userDetailViewModel);
                return Redirect("Checkout");
            }
            else
            {
                ModelState.AddModelError("", "error");
            }
            return View();
        }

        [Authorize]
        public ActionResult YourOrder()
        {
            List<OrderDetailViewModel> orderDetailViewModel = manageCartService.YourOrder(Convert.ToString(Session["Username"]));
            return View(orderDetailViewModel);
        }

        [Authorize]
        public ActionResult CancelOrder(int? OrderDetailId)
        {
            if(OrderDetailId == null)
            {
                return HttpNotFound();
            }
            manageCartService.CancelOrder(Convert.ToInt32(OrderDetailId));
            return Redirect("YourOrder");
        }

        [Authorize]
        public ActionResult OrderHistory()
        {
            List<OrderDetailViewModel> orderDetailViewModel = manageCartService.OrderHistory(Convert.ToString(Session["Username"]));
            return View(orderDetailViewModel);
        }

        [Authorize]
        public ActionResult Buy(int ProductId,string ProductName, decimal Price, string Description)
        {
            if (Session["Buy"] == null)
            {
                string Username = Convert.ToString(Session["Username"]);
                Session["Buy"] = manageCartService.Buy(ProductId, ProductName, Price, Description, Username);
                return Redirect("DisplayBuy");
            }
            else
            {
                TempData["Buy"]="AddtoCart to buy";
                return RedirectToAction("DisplayProduct","Product");
            }
        }

        public ActionResult BuyIncreaseQuantity()
        {
            Session["Buy"] = manageCartService.BuyIncreaseQuantity((List<CartViewModel>)Session["Buy"]);
            return RedirectToAction("DisplayBuy");
        }

        public ActionResult BuyDecreaseQuantity()
        {
            Session["Buy"] = manageCartService.BuyDecreaseQuantity((List<CartViewModel>)Session["Buy"]);
            return RedirectToAction("DisplayBuy");
        }

        public ActionResult DisplayBuy()
        {
            if(Session["Buy"] == null)
            {
                return RedirectToAction("DisplayProduct", "Product");
            }
            Session["Buy"] = manageCartService.DisplayBuy(Convert.ToString(Session["Username"]),(List<CartViewModel>)Session["Buy"]);
            return View((List<CartViewModel>) Session["Buy"]);
        }

        public ActionResult DeleteBuy()
        {
            Session["Buy"] = null;
            return RedirectToAction("DisplayProduct","Product");
        }

        public ActionResult ShippingDetail()
        {
            UserDetailViewModel userDetailViewModel = manageCartService.ShippingDetail(Convert.ToString(Session["Username"]));
            return View(userDetailViewModel);
        }

        [HttpPost]
        public ActionResult ShippingDetail(UserDetailViewModel userDetailViewModel)
        {
            if(ModelState.IsValid)
            {
                manageCartService.ShippingDetail(userDetailViewModel);
                return Redirect("DisplayBuy");
            }
            else
            {
                ModelState.AddModelError("", "error");
            }
            return View();
        }

        public ActionResult PlaceOrder()
        {
            bool value = manageCartService.CheckDetail(Convert.ToString(Session["Username"]));
            if(value)
            {
                return Redirect("ShippingDetail");
            }
            manageCartService.PlaceOrder((List<CartViewModel>)Session["Buy"]);
            return RedirectToAction("DisplayProduct","Product");
        }
    }

}
