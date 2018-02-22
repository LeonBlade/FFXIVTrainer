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
		public CharacterOffsets Character { get; set; }
	}

	[Serializable]
	public struct CharacterOffsets
	{
		public string Name { get; set; }
		public string Race { get; set; }
		public string Gender { get; set; }
		public string Clan { get; set; }
		public BodyOffsets Body { get; set; }
		public string NameHeight { get; set; }
	}

	[Serializable]
	public struct BodyOffsets
	{
		[XmlAttribute("Base")]
		public string Base { get; set; }

		public PositionOffsets Position { get; set; }
		public Vector3Offsets Bust { get; set; }
		public string Height { get; set; }
		public Vector3Offsets Scale { get; set; }
	}

	[Serializable]
	public struct PositionOffsets
	{
		public string X { get; set; }
		public string Y { get; set; }
		public string Z { get; set; }
		public string Rotation { get; set; }
	}

	[Serializable]
	public struct Vector3Offsets
	{
		[XmlAttribute("Base")]
		public string Base { get; set; }

		public string X { get; set; }
		public string Y { get; set; }
		public string Z { get; set; }
	}
}
