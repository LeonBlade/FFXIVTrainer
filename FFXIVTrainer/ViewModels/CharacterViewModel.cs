using FFXIVTrainer.Models;
using System.ComponentModel;
using System.Threading;
using Memory;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Text;

namespace FFXIVTrainer.ViewModels
{
	internal class CharacterViewModel : BaseViewModel
	{
		/// <summary>
		/// The character this ViewModel is referencing.
		/// </summary>
		public Character Character
		{
			get => (Character)model;
			set => model = value;
		}

		/// <summary>
		/// Initializes a new instance of the CharacterViewModel class.
		/// </summary>
		public CharacterViewModel()
		{
			// create a new instance of the Character model
			Character = new Character();

			// register the worker loop for this model
			Linkshell.Register("WORKER", Worker_DoWork);
		}

		/// <summary>
		/// The backgroundworker work loop
		/// </summary>
		private void Worker_DoWork()
		{
			// get the addresses for this loop
			var raceAddr = MemoryManager.GetAddressString(MemoryManager.Instance.BaseAddress, "8", Settings.Instance.Character.Race);
			var clanAddr = MemoryManager.GetAddressString(MemoryManager.Instance.BaseAddress, "8", Settings.Instance.Character.Clan);
			var genderAddr = MemoryManager.GetAddressString(MemoryManager.Instance.BaseAddress, "8", Settings.Instance.Character.Gender);
			var nameAddr = MemoryManager.GetAddressString(MemoryManager.Instance.BaseAddress, "8", Settings.Instance.Character.Name);

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
