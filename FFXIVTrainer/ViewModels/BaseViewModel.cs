using FFXIVTrainer.Models;

namespace FFXIVTrainer.ViewModels
{
	public class BaseViewModel
	{
		protected BaseModel model;
		protected Mediator mediator;

		public BaseViewModel(Mediator mediator)
		{
			this.mediator = mediator;
		}
	}
}
