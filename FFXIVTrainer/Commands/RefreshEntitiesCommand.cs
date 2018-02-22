using FFXIVTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FFXIVTrainer.Commands
{
	public class RefreshEntitiesCommand : ICommand
	{
		private EntityListViewModel entityListViewModel;
		public event EventHandler CanExecuteChanged;

		public RefreshEntitiesCommand(EntityListViewModel vm)
		{
			entityListViewModel = vm;
		}

		public bool CanExecute(object parameter)
		{
			return MemoryManager.Instance.IsReady();
		}

		public void Execute(object parameter)
		{
			entityListViewModel.Refresh();
		}
	}
}
