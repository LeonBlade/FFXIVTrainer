using FFXIVTrainer.Converters;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace FFXIVTrainer.Models
{
	public class Character : BaseModel
	{
		#region Character Enums

		/// <summary>
		/// The races of FFXIV
		/// </summary>
		[TypeConverter(typeof(EnumDescriptionTypeConverter))]
		public enum Races
		{
			Hyur = 1,
			Elezen,
			Lalafell,
			[Description("Miqo'te")]
			Miqote,
			Roegadyn,
			[Description("Au Ra")]
			AuRa
		}

		/// <summary>
		/// The clans of FFXIV
		/// </summary>
		[TypeConverter(typeof(EnumDescriptionTypeConverter))]
		public enum Clans
		{
			Midlander = 1,
			Highlander,
			Wildwood,
			Duskwight,
			Plainsfolk,
			Dunesfolk,
			[Description("Seeker of the Sun")]
			Sunseeker,
			[Description("Keeper of the Moon")]
			Moonkeeper,
			[Description("Sea Wolves")]
			Seawolves,
			Hellsguard,
			Raen,
			Xaela
		}

		/// <summary>
		/// Genders enum
		/// </summary>
		public enum Genders
		{
			Male,
			Female
		}

		#endregion
		
		public Address<string> Name { get; set; }
		public Address<Races> Race { get; set; }
		public Address<Clans> Clan { get; set; }
		public Address<Genders> Gender { get; set; }

		public Character()
		{
			Name = new Address<string>();
			Race = new Address<Races>();
			Clan = new Address<Clans>();
			Gender = new Address<Genders>();
		}
	}
}
