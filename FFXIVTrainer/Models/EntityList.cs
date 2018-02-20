using System.Collections.Generic;

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

		private Dictionary<int, string> names;
		public Dictionary<int, string> Names
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
			names = new Dictionary<int, string>();
		}
	}
}
