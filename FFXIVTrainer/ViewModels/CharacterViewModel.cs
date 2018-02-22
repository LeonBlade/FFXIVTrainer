using FFXIVTrainer.Models;
using System;
using System.Text;

namespace FFXIVTrainer.ViewModels
{
	public class CharacterViewModel : BaseViewModel
	{
		/// <summary>
		/// The character this ViewModel is referencing.
		/// </summary>
		public Character Character
		{
			get => (Character)model;
			set => model = value;
		}

		private string eOffset = "8";

		/// <summary>
		/// Initializes a new instance of the CharacterViewModel class.
		/// </summary>
		public CharacterViewModel(Mediator mediator) : base(mediator)
		{
			// create a new instance of the Character model
			Character = new Character();

			// listen to the work loop
			mediator.Work += Work;

			// listen to entity selection changes
			mediator.EntitySelection += (offset) => eOffset = offset;
		}

		/// <summary>
		/// The backgroundworker work loop
		/// </summary>
		private void Work()
		{
			try
			{
				// the base address for the current entity
				var baseAddr = MemoryManager.Add(MemoryManager.Instance.BaseAddress, eOffset);

				// get the addresses for this loop
				var raceAddr = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Race);
				var clanAddr = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Clan);
				var genderAddr = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Gender);
				var nameAddr = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Name);

				if (!Character.Race.freeze)
					Character.Race.value = (Character.Races)MemoryManager.Instance.MemLib.readByte(raceAddr);
				else
					MemoryManager.Instance.MemLib.writeBytes(raceAddr, Character.Race.GetBytes());

				if (!Character.Clan.freeze)
					Character.Clan.value = (Character.Clans)MemoryManager.Instance.MemLib.readByte(clanAddr);
				else
					MemoryManager.Instance.MemLib.writeBytes(clanAddr, Character.Clan.GetBytes());

				if (!Character.Gender.freeze)
					Character.Gender.value = (Character.Genders)MemoryManager.Instance.MemLib.readByte(genderAddr);
				else
					MemoryManager.Instance.MemLib.writeBytes(genderAddr, Character.Gender.GetBytes());

				if (!Character.Name.freeze)
				{
					var name = MemoryManager.Instance.MemLib.readString(nameAddr);

					if (name.IndexOf('\0') != -1)
						name = name.Substring(0, name.IndexOf('\0'));
					Character.Name.value = name;
				}
				else
				{
					Character.Name.value += '\0';
					MemoryManager.Instance.MemLib.writeBytes(nameAddr, Character.Name.GetBytes());
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message, "Oh no!");
				System.Windows.MessageBox.Show("Disabling " + this.GetType().Name, "Oh no!");
				mediator.Work -= Work;
			}
		}
	}
}
