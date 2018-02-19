namespace FFXIVTrainer.Models
{
	public class Bust : BaseModel
	{
		public Address<float> BustX { get; set; }
		public Address<float> BustY { get; set; }
		public Address<float> BustZ { get; set; }

		public Bust()
		{
			BustX = new Address<float>();
			BustY = new Address<float>();
			BustZ = new Address<float>();
		}
	}
}
