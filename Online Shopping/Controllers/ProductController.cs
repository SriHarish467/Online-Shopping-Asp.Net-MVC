using System.Collections.Generic;
using System.Web.Mvc;
using Online_Shopping.ServiceLayer;
using Online_Shopping.ViewModel;
using System;

namespace Online_Shopping.Controllers
{
    /// <summary>
    /// Product controller : To Display and Manage Products.
    /// Admin can create Products,Edit,Delete
    /// </summary>
    
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();

        // GET: Product
        public ActionResult DisplayProduct()
        {
            IEnumerable<ProductViewModel> productViewModel = productService.DisplayProduct();
            return View(productViewModel);
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

        public ActionResult EditProduct(int? ProductId)
        {
            if(ProductId == null)
            {
                return HttpNotFound();
            }
            ProductViewModel productViewModel = productService.EditProduct(Convert.ToInt32(ProductId));
            if (productViewModel != null)
            {
                return View(productViewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productService.EditProduct(productViewModel);
                return Redirect("DisplayProduct");
            }
            else
            {
                ModelState.AddModelError("", "error");
                return View(productViewModel);
            }
        }
        public ActionResult DeleteProduct(int? ProductId)
        {
            if(ProductId == null)
            {
                return HttpNotFound();
            }
            productService.DeleteProduct(Convert.ToInt32(ProductId));
            
            return View();
        }

    }
}