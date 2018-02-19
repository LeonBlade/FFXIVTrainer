using System;
using System.Xml.Serialization;

namespace FFXIVTrainer
{
	[XmlRoot]
	public class Settings
	{
		#region Singleton
		[XmlIgnore]
		private static Settings instance;
		[XmlIgnore]
		public static Settings Instance
		{
			get
			{
				if (instance == null)
					instance = new Settings();
				return instance;
			}
			set
			{
				instance = value;
			}
		}
		#endregion

		public string AoBOffset { get; set; }
		[XmlElement("Character")]
		public CharacterOffsets Character { get; set; }

		public Settings()
		{
			Character = new CharacterOffsets();
		}
	}

	[Serializable]
	public class CharacterOffsets
	{
		public string Name { get; set; }
		public string Race { get; set; }
		public string Gender { get; set; }
		public string Clan { get; set; }
		public PositionOffsets Position { get; set; }
		public string NameHeight { get; set; }

		public CharacterOffsets()
		{
			Position = new PositionOffsets();
		}
	}

	[Serializable]
	public class PositionOffsets
	{
		[XmlAttribute("Offset")]
		public string Base { get; set; }
		public string X { get; set; }
		public string Y { get; set; }
		public string Z { get; set; }
		public string Rotation { get; set; }
	}
}
