using Homies.Models;

namespace Homies.Contracts
{
	public interface IEventService
	{
		public Task<List< AllEventModelView>> GetAllAsync();
		//public Task Add(AddEventModelView model);
		public Task<FormAddEventViewModel> GetEventForm();
		public Task Add(FormAddEventViewModel model, string Userid);
		public Task<FormAddEventViewModel> GetEventFormForEdit(int id);
		public Task Edit(FormAddEventViewModel model);
		public Task<List<AllEventModelView>> GetMyEvents(string userId);

		public Task Join(int id, string userId);
		public Task Leave(int id, string userId);

    }
}
