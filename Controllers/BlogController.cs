using BlogApp.ActionFilter;
using BlogApp.Context;
using BlogApp.Models.Auth;
using BlogApp.Utility;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using BlogApp.Models;
using BlogApp.Data;

namespace BlogApp.Controllers;

public class BlogController(
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


	// Get all the available blog
	public ActionResult Index()
	{
		ViewBag.CurrentDateTime = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

		return View();
	}



	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Index(BlogDto blog)
	{
		if (ModelState.IsValid)
		{
			var mappedBlog = new BlogData()
			{
				AuthorId = blog.authorId,
			};
			bContext.Blogs.Add(mappedBlog); // Add the blog in the database

			bContext.SaveChanges(); // Save the changes in the database

			ViewBag.CurrentDateTime = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

			return RedirectToAction("Index");
		}

		return View(blog);
	}

	public ActionResult Create()
	{
		ViewBag.CurrentTime = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

		return View();
	}

	public PartialViewResult ListOfEmployee()
	{
		var list_of_blog = bContext.Blogs.OrderBy(x => x.Id);

		return PartialView(list_of_blog);
	}

	// Viewing the details of blog.
	public ActionResult Details(int id)
	{
		var blog_list = bContext.Blogs.Find(id);

		return View(blog_list);
	}

	// Function for edit
	public ActionResult Edit(int id)
	{
		var blog = bContext.Blogs.Find(id);

		ViewBag.CurrentTime = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

		return View(blog);
	}

	// Override the method of edit when submitted
	[HttpPost]
	public ActionResult Edit(BlogDto blog)
	{
		if (ModelState.IsValid)
		{
			bContext.Entry(blog).State = EntityState.Modified;

			bContext.SaveChanges();

			ViewBag.CurrentTime = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

			return RedirectToAction("Index");
		}

		return View(blog);
	}

	// Function for delete
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var blog_details = bContext.Blogs.Find(id);
		return View(blog_details);
	}

	// Delete (Update only the active column and set to 0)
	[HttpPost]
	public ActionResult Delete(int blogId, bool active = false)
	{
		var update_blog = bContext.Blogs.Where(x => x.Id == blogId).First();

		bContext.SaveChanges();

		return RedirectToAction("Index");
	}
}


