using System.Collections.ObjectModel;

namespace FFXIVTrainer.Models
{
	public class EntityList : BaseModel
	{
		private long size;
		public long Size
		{
			get => size;
			set => size = value;
		}

		private ObservableCollection<string> names;
		public ObservableCollection<string> Names
		{
			get => names;
			set => names = value;
		}

		private string selectedValue;
		public string SelectedValue
		{
			get => selectedValue;
			set => selectedValue = value;
		}

		private int selectedIndex;
		public int SelectedIndex
		{
			get => selectedIndex;
			set => selectedIndex = value;
		}

		public bool IsEnabled { get; set; }

		public EntityList()
		{
			names = new ObservableCollection<string>();
		}
	}
}
