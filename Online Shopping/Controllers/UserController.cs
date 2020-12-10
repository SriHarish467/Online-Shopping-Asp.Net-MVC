using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Online_Shopping.ServiceLayer;
using Online_Shopping.ViewModel;

namespace Online_Shopping.Controllers
{
    /// <summary>
    /// User controller : To Display user.
    /// Admin can Edit user roles
    /// </summary>
   
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        UserService userService = new UserService();
        // GET: User
        public ActionResult DisplayUser()
        {
            IEnumerable<UserViewModel> userViewModel = userService.DisplayUser();
            return View(userViewModel);
        }

        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("DisplayUser");
            }
            if (ModelState.IsValid)
            {
                EditUserViewModel editUserViewModel = userService.EditUser(Convert.ToInt32(id));
                if (editUserViewModel == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    IEnumerable<RoleViewModel> roleViewModel = userService.DisplayRole();
                    ViewBag.RoleName = new SelectList(roleViewModel, "RoleId", "RoleName");
                    return View(editUserViewModel);
                }

            }
            else
            {
                return RedirectToAction("DisplayUser");
            }
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                userService.EditUser(editUserViewModel);
                return RedirectToAction("DisplayUser");
            }
            else
            {
                return View(editUserViewModel);
            }
        }

        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                UserViewModel userViewModel = userService.DeleteUser(Convert.ToInt32(id));
                if (userViewModel == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(userViewModel);
                }
            }
            else
            {
                return RedirectToAction("DisplayUser");
            }
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult DeleteConfirmed(int id)
        {
            userService.DeleteConfirmed(id);
            return RedirectToAction("DisplayProduct");
        }

        public ViewResult DisplayRole()
        {
           IEnumerable<RoleViewModel> roleViewModel = userService.DisplayRole();
            return View(roleViewModel);
        }
    }
}