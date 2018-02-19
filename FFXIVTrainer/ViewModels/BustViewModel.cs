using FFXIVTrainer.Models;

namespace FFXIVTrainer.ViewModels
{
	public class BustViewModel : BaseViewModel
	{
		public Bust Bust
		{
			get => (Bust)model;
			set => model = value;
		}

		public BustViewModel()
		{
			Bust = new Bust();

			Linkshell.Register("WORKER", Work);
		}

		public void Work()
		{
			var baseAddr = MemoryManager.Instance.BaseAddress;
			var body = Settings.Instance.Character.Body;
			var bust = body.Bust;
			var mem = MemoryManager.Instance.MemLib;

			var xAddr = MemoryManager.GetAddressString(baseAddr, "8", body.Base, bust.Base, bust.X);
			var yAddr = MemoryManager.GetAddressString(baseAddr, "8", body.Base, bust.Base, bust.Y);
			var zAddr = MemoryManager.GetAddressString(baseAddr, "8", body.Base, bust.Base, bust.Z);

			if (Bust.BustX.freeze)
				mem.writeBytes(xAddr, Bust.BustX.GetBytes());
			else
				Bust.BustX.value = mem.readFloat(xAddr);

			if (Bust.BustY.freeze)
				mem.writeBytes(yAddr, Bust.BustY.GetBytes());
			else
				Bust.BustY.value = mem.readFloat(yAddr);

			if (Bust.BustZ.freeze)
				mem.writeBytes(zAddr, Bust.BustZ.GetBytes());
			else
				Bust.BustZ.value = mem.readFloat(zAddr);
		}
	}
}
