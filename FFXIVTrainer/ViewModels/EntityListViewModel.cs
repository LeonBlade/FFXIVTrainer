using FFXIVTrainer.Models;
using System.ComponentModel;

namespace FFXIVTrainer.ViewModels
{
	public class EntityListViewModel : BaseViewModel
	{
		public EntityList EntityList { get => (EntityList)model; }

		public EntityListViewModel(Mediator mediator) : base(mediator)
		{
			// instantiate a new model
			model = new EntityList();

			// respond to property changes
			model.PropertyChanged += Model_PropertyChanged;
		}

		/// <summary>
		/// Model property changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//if (e.PropertyName == "SelectedValue")
				//Linkshell.Emit("EntityList.Changed", new Actions.EntityEventArgs(EntityList.Names[(EntityList.SelectedIndex + 1) * 8]));
		}

		public void Work(object args)
		{
			// get the array size
			EntityList.Size = MemoryManager.Instance.MemLib.readLong(MemoryManager.Instance.BaseAddress + ",0x0");

			// dump the list
			EntityList.Names.Clear();

			// loop over entity list size
			for (var i = 0; i < EntityList.Size; i++)
			{
				var addr = MemoryManager.GetAddressString(MemoryManager.Instance.BaseAddress, ((i + 1) * 8).ToString("X"), Settings.Instance.Character.Name);
				var name = MemoryManager.Instance.MemLib.readString(addr);
				EntityList.Names.Add(((i + 1) * 8), name.Substring(0, name.IndexOf('\0')));
			}

			// set the enable state
			EntityList.IsEnabled = true;
			if (EntityList.SelectedValue == null)
				EntityList.SelectedValue = EntityList.Names[8];
		}
	}
}
