﻿using BlogApp.ActionFilter;
using BlogApp.Context;
using BlogApp.Models.Auth;
using BlogApp.Utility;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

namespace BlogApp.Controllers;

public class AuthController(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    INotyfService notyf,
    BContext bContext,
    IHttpContextAccessor httpContextAccessor) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly INotyfService _notyfService = notyf;
    private readonly BContext _bContext = bContext;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    [RedirectAuthenticatedUsers]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Username);

            var result = await _signInManager.PasswordSignInAsync(user!.UserName!, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userDetails = await Helper.GetCurrentUserIdAsync(_httpContextAccessor, _userManager);


                _notyfService.Success("Login succesful");
                return RedirectToAction("Index", "Blog");
            }

            ModelState.AddModelError("", "Invalid login attempt");
            _notyfService.Error("Invalid login attempt");
            return View(model);
        }

        return View(model);
    }

    [RedirectAuthenticatedUsers]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email || u.UserName == model.Username);

            if (existingUser != null)
            {
                _notyfService.Warning("User already exist!");
                return View();
            }

            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                _notyfService.Error("An error occured while registering user!");
                return View();
            }

            _notyfService.Success("Registration was successful");
            //await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Login");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Login", "Auth");
    }
}
