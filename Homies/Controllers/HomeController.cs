﻿using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homies.Controllers
{
	public class HomeController : BaseController
	{
		[AllowAnonymous]
		public IActionResult Index()
		{
			if (User?.Identity?.IsAuthenticated ?? false)
			{
				return RedirectToAction("All", "Event");
			}

			return View();
		}

	}
}