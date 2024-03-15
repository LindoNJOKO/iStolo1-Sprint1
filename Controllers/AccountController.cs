using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Firebase.Auth;
using Newtonsoft.Json;
using iStolo1.Models;

namespace iStolo1.Controllers
{
    public class AccountController : Controller
    {
        private FirebaseAuthProvider _auth;

        public AccountController()
        {
            _auth = new FirebaseAuthProvider(new FirebaseConfig(Environment.GetEnvironmentVariable("FirebaseMathApp")));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                var authLink = await _auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.AccountPassword);
                string currentUserId = authLink.User.LocalId;

                if (currentUserId != null)
                {
                    //After successful registration
                    return RedirectToAction("Main", "Home");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
                ModelState.AddModelError(string.Empty, firebaseEx.error.message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var authLink = await _auth.SignInWithEmailAndPasswordAsync(model.Username, model.AccountPassword);
                string currentUserId = authLink.User.LocalId;

                if (currentUserId != null)
                {
                    // Your logic after successful login
                    return RedirectToAction("Main", "Home");
                }

            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
                ModelState.AddModelError(string.Empty, firebaseEx.error.message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            // Implement logout logic here
            return RedirectToAction("Login");
        }

        // Other action methods...

        [HttpPost]
        public async Task<IActionResult> AddInventoryItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Implement logic to add item to inventory
                return RedirectToAction("Inventory", "Home");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuantity(int itemId, int quantity)
        {
            // Implement logic to add quantity to an item in inventory
            return RedirectToAction("Inventory", "Home");
        }
    }
}
