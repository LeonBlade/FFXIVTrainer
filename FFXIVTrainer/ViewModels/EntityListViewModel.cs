using FFXIVTrainer.Commands;
using FFXIVTrainer.Models;
using System;
using System.ComponentModel;

namespace FFXIVTrainer.ViewModels
{
	public class EntityListViewModel : BaseViewModel
	{
		public EntityList EntityList { get => (EntityList)model; }

		private RefreshEntitiesCommand refreshEntitiesCommand;
		public RefreshEntitiesCommand RefreshEntitiesCommand
		{
			get => refreshEntitiesCommand;
		}

		public EntityListViewModel(Mediator mediator) : base(mediator)
		{
			// instantiate a new model
			model = new EntityList();

			// respond to property changes
			model.PropertyChanged += Model_PropertyChanged;

			// initialize the refresh entities command
			refreshEntitiesCommand = new RefreshEntitiesCommand(this);

			// refresh the list initially
			this.Refresh();
		}

		/// <summary>
		/// Model property changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedIndex")
				mediator.SendEntitySelection(((EntityList.SelectedIndex + 1) * 8).ToString("X"));
		}

		/// <summary>
		/// Refresh the entity list
		/// </summary>
		public void Refresh()
		{
			try
			{
				// get the array size
				EntityList.Size = MemoryManager.Instance.MemLib.readLong(MemoryManager.Instance.BaseAddress);

				// clear the entity list
				EntityList.Names.Clear();

				// loop over entity list size
				for (var i = 0; i < EntityList.Size; i++)
				{
					var addr = MemoryManager.GetAddressString(MemoryManager.Add(MemoryManager.Instance.BaseAddress, ((i + 1) * 8).ToString("X")), Settings.Instance.Character.Name);
					var name = MemoryManager.Instance.MemLib.readString(addr);
					if (name.IndexOf('\0') != -1)
						name = name.Substring(0, name.IndexOf('\0'));
					EntityList.Names.Add(name);
				}

				// set the enable state
				EntityList.IsEnabled = true;
				// set the index if its under 0
				if (EntityList.SelectedIndex < 0)
					EntityList.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.ToString());
			}
		}
	}
}
