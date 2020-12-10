using Online_Shopping.ServiceLayer;
using Online_Shopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace Online_Shopping.Controllers
{
    /// <summary>
    /// Account controller : To manage user registration and login.
    /// Allows user to register if they are new; login if they are registered users.
    /// </summary>
    
    public class AccountController : Controller
    {
        AccountService accountService = new AccountService();
        ManageCartService manageCartService = new ManageCartService();
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                bool existingusername = accountService.ExistingUserSignUp(userViewModel);
                if (!existingusername)
                {
                    bool existingemail = accountService.ExistingEmailSignUp(userViewModel);
                    if (!existingemail)
                    {
                        accountService.NewUserSignUp(userViewModel);
                        FormsAuthentication.SetAuthCookie(userViewModel.Username, false);
                        Session["Username"] = userViewModel.Username.ToString();
                        Session["Cart"]=manageCartService.UpdateCart(userViewModel.Username.ToString(),(List<CartViewModel>)Session["Cart"]);
                        return RedirectToAction("DisplayProduct","Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Id already exist");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username already exist");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                bool value = accountService.Login(loginViewModel);
                if(value)
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.Username, false);
                    Session["Username"] = loginViewModel.Username.ToString();
                    Session["Count"] = manageCartService.UpdateCart(loginViewModel.Username.ToString(),(List<CartViewModel>) Session["Cart"]);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("DisplayProduct", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Username"] = null;
            Session["Cart"] = null;
            Session["Count"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdateProfile()
        {
            var name = Convert.ToString(Session["Username"]);
            UpdateUserProfileViewModel updateUserProfileViewModel = accountService.UpdateProfile(name);
            return View(updateUserProfileViewModel);
        }

        [HttpPost]
        public ActionResult UpdateProfile(UpdateUserProfileViewModel updateUserProfileViewModel)
        {
            if(ModelState.IsValid)
            {
                accountService.UpdateProfile(updateUserProfileViewModel);
                TempData["Success"] = "Profile Updated";
                return RedirectToAction("DisplayProduct","Product");
            }
            else
            {
                return View();
            }
        }

        public ViewResult ChangePassword()
        {
            var name = Convert.ToString(Session["Username"]);
            UserViewModel userViewModel = accountService.ChangePassword(name);
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                accountService.ChangePassword(userViewModel);
                return RedirectToAction("UpdateProfile");
            }
            else
            {
                ModelState.AddModelError("", "error");
                return View();
            }
        }
    }
}