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

		#region Properties

		private string name;
		/// <summary>
		/// Character name 32-bytes long maximum
		/// </summary>
		public string Name
		{
			get => name;
			set => name = value;
		}

		private byte race;
		/// <summary>
		/// Character race
		/// </summary>
		public Races Race
		{
			get { return (Races)race; }
			set { race = (byte)value; }
		}

		private byte clan;
		/// <summary>
		/// Character clan
		/// </summary>
		public Clans Clan
		{
			get { return (Clans)clan; }
			set { clan = (byte)value; }
		}

		private byte gender;
		/// <summary>
		/// Character gender
		/// </summary>
		public Genders Gender
		{
			get { return (Genders)gender; }
			set { gender = (byte)value; }
		}

		private Position position;
		/// <summary>
		/// Position of the player
		/// </summary>
		public Position Position
		{
			get => position;
			set => position = value;
		}

		#endregion

		#region Freeze States

		public bool NameFreeze { get; set; }
		public bool RaceFreeze { get; set; }
		public bool ClanFreeze { get; set; }
		public bool GenderFreeze { get; set; }
		public bool PositionFreeze { get; set; }

		#endregion
	}

	#region Suplementary classes for Character

	/// <summary>
	/// Position Class to hold values for character position
	/// </summary>
	public class Position : INotifyPropertyChanged
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float Rotation { get; set; }

#pragma warning disable 67
		public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67
	}

	#endregion
}
