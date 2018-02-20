using FFXIVTrainer.Models;

namespace FFXIVTrainer.ViewModels
{
	public class BaseViewModel
	{
		protected BaseModel model;
		private Mediator mediator;

		public BaseViewModel(Mediator mediator)
		{
			this.mediator = mediator;
		}
	}
}
