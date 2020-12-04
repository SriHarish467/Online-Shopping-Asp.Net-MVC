using System.Collections.Generic;
using System.Web.Mvc;
using Online_Shopping.ServiceLayer;
using Online_Shopping.DomainModel;
using Online_Shopping.ViewModel;
using System;


namespace Online_Shopping.Controllers
{
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();
        
        ManageCartService manageCartService = new ManageCartService();
        
        // GET: Product
        public ActionResult DisplayProduct()
        {
            IEnumerable<Product> product = productService.DisplayProduct();
            return View(product);
        }

        public ViewResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productService.CreateProduct(productViewModel);
                return Redirect("DisplayProduct");
            }
            else
            {
                ModelState.AddModelError("", "error");
                return View();
            }
        }

        public ActionResult EditProduct(int id)
        {
           
            Product product = productService.EditProduct(id);
            if(product != null)
            {
                return View(product);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productService.EditProduct(product);
                return Redirect("DisplayProduct");
            }
            else
            {
                ModelState.AddModelError("", "error");
                return View(product);
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            Product product = productService.DeleteProduct(id);
            if(product != null)
            {
                return View(product);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("DeleteProduct")]
        public ActionResult DeleteConfirmed(int id)
        {
            productService.DeleteConfirmed(id);
            return Redirect("DisplayProduct");
        }

        public ActionResult AddCart(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            List<CartViewModel> data = new List<CartViewModel>();
           if (Session["CartViewModel"] == null)
            {
               Session["CartViewModel"] = manageCartService.AddtoCart(Convert.ToInt32(id),data);
            }
            else
            {
                 data = Session["CartViewModel"] as List<CartViewModel>;
                manageCartService.AddtoCart(Convert.ToInt32(id), data);
            }

            return RedirectToAction("DisplayProduct");
        }

        public ActionResult DisplayCart()
        {
            List<CartViewModel> cart = (List<CartViewModel>)Session["CartViewModel"];
            return View(cart);
        }
    }
}