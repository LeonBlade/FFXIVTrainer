using FFXIVTrainer.Models;

namespace FFXIVTrainer.ViewModels
{
	public class EntityListViewModel : BaseViewModel
	{
		public EntityList EntityList
		{
			get => (EntityList)model;
		}

		public EntityListViewModel()
		{
			model = new EntityList();

			// register the worker loop
			Linkshell.Register("WORKER", Work);
		}

		public void Work()
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
