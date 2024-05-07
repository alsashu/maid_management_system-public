using MaidManagementSolutions.DataAccessLayer;
using MaidManagementSolutions.ViewModel;
using MaidManagementSolutions.Web.Halper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MaidManagementSolutions.Web.Areas.Client.Controllers
{
    [Area("client")]
    public class AccountController : Controller
    {
        [ActionName("login")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string id=string.Empty;
                    string username = string.Empty;

                    if (IsValidUser(model.PhoneNumber, model.Password, out id,out username))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.MobilePhone, model.PhoneNumber),
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Actor, id),
                            // Add other claims as needed
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("searchmaid", "home", new { area = "business" });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home", "Error");
            }
        }
        [ActionName("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home", new { area = "business" });
        }
        [ActionName("registration")]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        if (!IsValidUser(model.PhoneNumber))
                        {
                            List<string> listPassword = new List<string>();
                            var param = new
                            {
                                @id = model.Id,
                                @username = model.Username,
                                @password = CryptoHelper.CalculateMD5Hash(model.Password),
                                @phonenumber = model.PhoneNumber,
                                @istermsaccepted = model.IsTermsAccepted,
                                @registrationdate = DateTime.Now,
                                @isativeuser = true,
                                @oldpassword = JsonConvert.SerializeObject(listPassword),
                                @updatedon = DateTime.Now
                            };
                            var res = DataAccess.ExecuteScalar(SqlStatement.addUser, param);
                            return RedirectToAction(nameof(Login));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User already register with this number.");
                    }
                    // Add user to database
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home", "Error");
            }
        }
        private bool IsValidUser(string phNum, string password, out string id, out string username)
        {
            bool success = false;
            try
            {
                var param = new
                {
                    @phonenumber = phNum,
                    @password = CryptoHelper.CalculateMD5Hash(password)
                };
                var dt = DataAccess.GetDataTable(SqlStatement.getActiveUser, param);
                if (dt.Rows.Count > 0)
                {
                    success = true;
                }
                id = dt.Rows[0]["id"].ToString();
                username = dt.Rows[0]["username"].ToString();
                return success;
            }
            catch (Exception ex)
            {
                id = string.Empty;
                username = string.Empty;
                return success;
            }
        }

        private bool IsValidUser(string phNum)
        {
            bool success = false;
            try
            {
                var param = new
                {
                    @phonenumber = phNum
                };
                var dt = DataAccess.GetDataTable(SqlStatement.isUserAvailable, param);
                if (dt.Rows.Count > 0)
                {
                    success = true;
                }
                return success;
            }
            catch (Exception ex)
            {
                return success;
            }
        }
    }
}
