﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ESAQAApp.Models;
using System;

namespace ESAQAApp.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }


        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //[Authorize(Roles = "Admin, CanEditUser")]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin, CanEditUser")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = model.GetUser();
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    AspNetUserMirror anum = new AspNetUserMirror();
                    anum.UserName = user.UserName;
                    anum.FirstName = user.FirstName;
                    anum.LastName = user.LastName;
                    anum.Password = model.Password;
                    anum.Email = user.Email;

                    _db.AspNetUserMirror.Add(anum);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index", "Account");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize(Roles = "Admin, CanEditUser, User")]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, CanEditUser, User")]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            try
            {
                bool hasPassword = HasPassword();
                ViewBag.HasLocalPassword = hasPassword;
                ViewBag.ReturnUrl = Url.Action("Manage");
                var currentUser = User.Identity.Name;
                if (hasPassword)
                {
                    if (ModelState.IsValid)
                    {
                        IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                        if (result.Succeeded)
                        {
                            var userMirror = _db.AspNetUserMirror.First(u => u.UserName == currentUser);
                            userMirror.Password = model.ConfirmPassword;
                            _db.Entry(userMirror).State = System.Data.Entity.EntityState.Modified;
                            await _db.SaveChangesAsync();

                            TempData["Message"] = ShowMessage(Sign.Success, "Your password has been changed successfully.");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            var error = result.Errors.ToArray()[0].ToString();
                            TempData["Message"] = ShowMessage(Sign.Error, error);
                            //AddErrors(result.Errors.ToString());
                        }
                    }
                }
                else
                {
                    // User does not have a password so remove any validation errors caused by a missing OldPassword field
                    ModelState state = ModelState["OldPassword"];
                    if (state != null)
                    {
                        state.Errors.Clear();
                    }

                    if (ModelState.IsValid)
                    {
                        IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                        if (result.Succeeded)
                        {
                            var userMirror = _db.AspNetUserMirror.First(u => u.UserName == currentUser);
                            userMirror.Password = model.ConfirmPassword;
                            _db.Entry(userMirror).State = System.Data.Entity.EntityState.Modified;
                            await _db.SaveChangesAsync();

                            TempData["Message"] = ShowMessage(Sign.Success, "Your password has been changed successfully.");
                            return RedirectToAction("Index");
                            //return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                }

                // If we got this far, something failed, redisplay form
                return View(model);

            }
            catch (Exception ex)
            {
                return View(model);
                var message = ExtractInnerException(ex);
                TempData["Message"] = ShowMessage(Sign.Danger, message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Admin, CanEditGroup, CanEditUser")]
        public ActionResult Index()
        {
            var users = _db.Users;
            var model = new List<EditUserViewModel>();
            foreach (var user in users)
            {
                var u = new EditUserViewModel(user);
                model.Add(u);
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, CanEditUser")]
        public ActionResult Edit(string id, ManageMessageId? Message = null)
        {
            var user = _db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            ViewBag.MessageId = Message;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, CanEditUser")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _db.Users.First(u => u.UserName == model.UserName);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    await _db.SaveChangesAsync();

                    var userMirror = _db.AspNetUserMirror.First(u => u.UserName == model.UserName);
                    userMirror.FirstName = model.FirstName;
                    userMirror.LastName = model.LastName;
                    userMirror.Email = user.Email;
                    _db.Entry(userMirror).State = System.Data.Entity.EntityState.Modified;
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var message = ExtractInnerException(ex);
                    TempData["Message"] = ShowMessage(Sign.Danger, message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize(Roles = "Admin, CanEditUser")]
        public ActionResult Delete(string id = null)
        {
            var user = _db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, CanEditUser")]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = _db.Users.First(u => u.UserName == id);
            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, CanEditUser")]
        public ActionResult UserGroups(string id)
        {
            var user = _db.Users.First(u => u.UserName == id);
            var model = new SelectUserGroupsViewModel(user);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, CanEditUser")]
        [ValidateAntiForgeryToken]
        public ActionResult UserGroups(SelectUserGroupsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var idManager = new IdentityManager();
                var user = _db.Users.First(u => u.UserName == model.UserName);
                idManager.ClearUserGroups(user.Id);
                foreach (var group in model.Groups)
                {
                    if (group.Selected)
                    {
                        idManager.AddUserToGroup(user.Id, group.GroupId);
                    }
                }
                return RedirectToAction("index");
            }
            return View();
        }

        [Authorize(Roles = "Admin, CanEditRole, CanEditGroup, User")]
        public ActionResult UserPermissions(string id)
        {
            var user = _db.Users.First(u => u.UserName == id);
            var model = new UserPermissionsViewModel(user);
            return View(model);
        }

        public ActionResult Denied()
        {
            return View();
        }

        #region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}