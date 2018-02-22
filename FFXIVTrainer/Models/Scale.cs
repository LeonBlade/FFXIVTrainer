namespace FFXIVTrainer.Models
{
    public class Scale : BaseModel
    {
		public Address<float> ScaleX { get; set; }
		public Address<float> ScaleY { get; set; }
		public Address<float> ScaleZ { get; set; }

		public Scale()
		{
			ScaleX = new Address<float>();
			ScaleY = new Address<float>();
			ScaleZ = new Address<float>();
		}
	}
}
