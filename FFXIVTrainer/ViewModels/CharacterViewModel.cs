using FFXIVTrainer.Models;
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

			// register the worker loop for this model
			//Linkshell.Register("WORKER", Work);
			// register for when entity updates
			//Linkshell.Register("EntityList.Updated", (BaseEventArgs args) => eOffset = ((EntityEventArgs)args).Offset);
		}

		/// <summary>
		/// The backgroundworker work loop
		/// </summary>
		private void Work()
		{
			// the base address for the current entity
			//var baseAddr = "0x" + MemoryManager.Instance.BaseAddress + "+0x" + eOffset;
			var baseAddr = "0x7ff62baf8bC0";

			// get the addresses for this loop
			var raceAddr = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Race);
			var clanAddr = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Clan);
			var genderAddr = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Gender);
			var nameAddr = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Name);

			if (!Character.RaceFreeze)
				Character.Race = (Character.Races)MemoryManager.Instance.MemLib.readByte(raceAddr);
			else
				MemoryManager.Instance.MemLib.writeBytes(raceAddr, new byte[] { (byte)Character.Race });

			if (!Character.ClanFreeze)
				Character.Clan = (Character.Clans)MemoryManager.Instance.MemLib.readByte(clanAddr);
			else
				MemoryManager.Instance.MemLib.writeBytes(clanAddr, new byte[] { (byte)Character.Clan });

			if (!Character.GenderFreeze)
				Character.Gender = (Character.Genders)MemoryManager.Instance.MemLib.readByte(genderAddr);
			else
				MemoryManager.Instance.MemLib.writeBytes(genderAddr, new byte[] { (byte)Character.Gender });

			if (!Character.NameFreeze)
				Character.Name = MemoryManager.Instance.MemLib.readString(nameAddr).Trim('\0');
			else
				MemoryManager.Instance.MemLib.writeBytes(nameAddr, Encoding.UTF8.GetBytes(Character.Name + '\0'));
		}
	}
}
