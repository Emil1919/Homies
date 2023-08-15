using Homies.Contracts;
using Homies.Data;
using Homies.Models;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services
{
	
	public class EventService : IEventService
	{
		private readonly HomiesDbContext context;
        public EventService(HomiesDbContext context)
        {
            this.context = context;
        }
        public async Task<List<AllEventModelView>> GetAllAsync()
		{
			List <AllEventModelView> events =  context.Events.Select(e => new AllEventModelView
			{
				Id = e.Id,
				Name = e.Name,
				Description = e.Description,
				OrganiserId = e.OrganiserId,
				CreatedOn = e.CreatedOn,
				Start = e.Start,
				End = e.End,
				Type = e.Type.Name,
				Organiser = e.Organiser.UserName
			}).ToList();
			
			return events;
		}

		public async Task<FormAddEventViewModel> GetEventForm()
		{
			List<TypeOfEventViewModel> types = context.Types.Select(t=> new TypeOfEventViewModel
			{
				Id = t.Id,
				Name = t.Name
			}).ToList();

			FormAddEventViewModel model = new FormAddEventViewModel
			{
				Types = types
			};

			return model;
		}
	
		public async Task Add(FormAddEventViewModel model, string Userid)
		{
			Event newEvent = new Event
			{
				Name = model.Name,
				Description = model.Description,
				OrganiserId = Userid,
				CreatedOn = model.CreatedOn,
				Start = model.Start,
				End = model.End,
				TypeId = model.TypeId
			};
			 context.Events.Add(newEvent);
			 context.SaveChanges();
		}

		public async Task<FormAddEventViewModel> GetEventFormForEdit(int id)
		{
			Event @event = context.Events.Include(e => e.Type).FirstOrDefault(e => e.Id == id);

			FormAddEventViewModel model = new FormAddEventViewModel
			{
				
				Id = @event.Id,
				Name = @event.Name,
				Description = @event.Description,
				CreatedOn = @event.CreatedOn,
				Start = @event.Start,
				End = @event.End,
				TypeId = @event.TypeId,
				Types = context.Types.Select(t => new TypeOfEventViewModel
				{
					Id = t.Id,
					Name = t.Name
				}).ToList()
			};
			return model;
		}
		public async Task Edit(FormAddEventViewModel model)
		{
			Event @event = context.Events.FirstOrDefault(e => e.Id == model.Id);
			@event.Name = model.Name;
			@event.Description = model.Description;
			@event.CreatedOn = model.CreatedOn;
			@event.Start = model.Start;
			@event.End = model.End;
			@event.TypeId = model.TypeId;
			
			context.SaveChanges();
		}

        public Task<List<AllEventModelView>> GetMyEvents(string userId)
        {
            List<AllEventModelView> events = context.EventParticipants.Where(e => e.HelperId == userId).Select(e => new AllEventModelView
			{
                Id = e.Event.Id,
                Name = e.Event.Name,
                Description = e.Event.Description,
                OrganiserId = e.Event.OrganiserId,
                CreatedOn = e.Event.CreatedOn,
                Start = e.Event.Start,
                End = e.Event.End,
                Type = e.Event.Type.Name,
                Organiser = e.Event.Organiser.UserName
            }).ToList();
			return Task.FromResult(events);
        }

        public Task Join(int id, string userId)
        {
            if(context.EventParticipants.Any(ep => ep.EventId == id && ep.HelperId == userId))
			{
                return Task.CompletedTask;
            }
			EventParticipant eventParticipant = new EventParticipant
			{
                EventId = id,
                HelperId = userId
            };
			context.EventParticipants.Add(eventParticipant);
			context.SaveChanges();
			return Task.CompletedTask;
        }

        public Task Leave(int id, string userId)
        {
            if (!context.EventParticipants.Any(ep => ep.EventId == id && ep.HelperId == userId))
			{
                return Task.CompletedTask;
            }
			EventParticipant eventParticipant = context.EventParticipants.FirstOrDefault(ep => ep.EventId == id && ep.HelperId == userId);
			context.EventParticipants.Remove(eventParticipant);
			context.SaveChanges();
			return Task.CompletedTask;

        }
    }
}
