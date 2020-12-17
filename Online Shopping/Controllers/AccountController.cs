using Online_Shopping.ServiceLayer;
using Online_Shopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
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
                bool existingusername = accountService.ExistingUserSignUp(userViewModel.Username);
                if (!existingusername)
                {
                    bool existingemail = accountService.ExistingEmailSignUp(userViewModel.EmailId);
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
                bool value = accountService.Login(loginViewModel.Username, loginViewModel.Password);
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

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailId)
        {
            try
            {
                bool value = accountService.ExistingEmailSignUp(EmailId);
                if (value)
                {
                    Guid guid = Guid.NewGuid();
                    accountService.UpdateUser(EmailId,Convert.ToString(guid));
                    //Session["Email"] = EmailId;
                    //Random rdm = new Random();
                    //Session["otp"] = rdm.Next(1000, 9999);
                    string senderEmail = ConfigurationManager.AppSettings["SenderEmail"];
                    string senderPassword = ConfigurationManager.AppSettings["SenderPassword"];
                    MailMessage mailMessage = new MailMessage(senderEmail, EmailId);
                    mailMessage.Subject = "Forgot Password";
                    // mailMessage.Body = Convert.ToString(Session["otp"]);
                    mailMessage.Body = "https://localhost:44321/Account/NewPassword/" + guid.ToString();
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    //{
                    //    UserName = Username,
                    //    Password = Password
                    //};
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }
                else
                {

                }
                //return Redirect("Code");
                return View();
            }
            catch
            {
                
            }
            return View();
        }

        public ActionResult Code()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Code(string otp)
        {
            if(Convert.ToString(Session["otp"]) == otp)
            {
                Session["otp"] = null;
                return Redirect("Newpassword");
            }
            else
            {
                return View();
            }
        }

        public ActionResult NewPassword(string id)
        {
            // NewPasswordViewModel newPasswordViewModel = accountService.NewPassword(Convert.ToString(Session["Email"]));
            NewPasswordViewModel newPasswordViewModel = accountService.Newpassword(id);
            if(newPasswordViewModel != null)
            {
                return View(newPasswordViewModel);
            }
            else
            {
                return HttpNotFound();
            }
           // return View(newPasswordViewModel);
        }

        [HttpPost]
        public ActionResult NewPassword(NewPasswordViewModel newPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                accountService.NewPassword(newPasswordViewModel);
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "error");
                return View();
            }
        }
    }
}