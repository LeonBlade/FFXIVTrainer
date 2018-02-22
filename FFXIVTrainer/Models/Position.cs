namespace FFXIVTrainer.Models
{
    public class Position : BaseModel
    {
		public Address<float> X { get; set; }
		public Address<float> Y { get; set; }
		public Address<float> Z { get; set; }
		public Address<float> Rotation { get; set; }

		public Position()
		{
			X = new Address<float>();
			Y = new Address<float>();
			Z = new Address<float>();
			Rotation = new Address<float>();
		}
    }
}
