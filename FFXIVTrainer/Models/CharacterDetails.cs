namespace FFXIVTrainer.Models
{
    public class CharacterDetails : BaseModel
    {
		public Address<float> Height { get; set; }
		public Address<float> Muscle { get; set; }
		public Address<float> TailSize { get; set; }
		public Address<byte> TailType { get; set; }
		public Address<byte> Head { get; set; }
		public Address<byte> Hair { get; set; }
		public Address<byte> Eyes { get; set; }
		public Address<byte> Nose { get; set; }
		public Address<byte> Lips { get; set; }

		public CharacterDetails()
		{
			Height = new Address<float>();
			Muscle = new Address<float>();
			TailSize = new Address<float>();
			TailType = new Address<byte>();
			Head = new Address<byte>();
			Hair = new Address<byte>();
			Eyes = new Address<byte>();
			Nose = new Address<byte>();
			Lips = new Address<byte>();
		}
    }
}
