using Homies.Contracts;
using Homies.Models;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Homies.Controllers
{
	public class EventController : BaseController
	{
		
		private readonly IEventService eventService;
        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }



        public async Task <IActionResult> All()
		{
			List<AllEventModelView> events =  eventService.GetAllAsync().Result;

			return View(events);
		}
		[HttpGet]
		public async Task <IActionResult> Add()
		{
			FormAddEventViewModel model =  await eventService.GetEventForm();

			return View(model);
		}
		[HttpPost]
		public async Task <IActionResult> Add(FormAddEventViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Add", "Event",model);
			}
			string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			eventService.Add(model , userId);

			return RedirectToAction("All");
		}
		[HttpGet]
		public async Task <IActionResult> Edit(int id)
		{
			FormAddEventViewModel model =  eventService.GetEventFormForEdit(id).Result;

			return View(model);
		}
		[HttpPost]
		public async Task <IActionResult> Edit(FormAddEventViewModel model)
		{
            if (!ModelState.IsValid)
			{
                return RedirectToAction("Edit", "Event",model);
            }
            eventService.Edit(model);

            return RedirectToAction("All");
        }
		[HttpGet]
		public async Task <IActionResult> Joined()
		{
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			List<AllEventModelView> events =await  eventService.GetMyEvents(userId);
            
			return View(events);
        }
		[HttpPost]
		public async Task <IActionResult> Join(int id)
		{
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            eventService.Join(id, userId);

            return RedirectToAction("Joined");
        }
		[HttpPost]
		public async Task <IActionResult> Leave(int id)
		{
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            eventService.Leave(id, userId);

            return RedirectToAction("Joined");
        }
		[HttpGet]
		public async Task <IActionResult> Details(int id)
		{
            AllEventModelView model =  eventService.GetAllAsync().Result.FirstOrDefault(x => x.Id == id);

            return View(model);
        }
		
	}
}
