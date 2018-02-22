using FFXIVTrainer.Models;
using System;

namespace FFXIVTrainer.ViewModels
{
	public class BustViewModel : BaseViewModel
	{
		public Bust Bust
		{
			get => (Bust)model;
			set => model = value;
		}

		private string eOffset = "8";

		public BustViewModel(Mediator mediator) : base(mediator)
		{
			Bust = new Bust();

			// listen to the work loop
			mediator.Work += Work;

			// listen to the entity change
			mediator.EntitySelection += offset => eOffset = offset;
		}

		public void Work()
		{
			try
			{
				var baseAddr = MemoryManager.Add(MemoryManager.Instance.BaseAddress, eOffset);
				var body = Settings.Instance.Character.Body;
				var bust = body.Bust;
				var mem = MemoryManager.Instance.MemLib;

				var xAddr = MemoryManager.GetAddressString(baseAddr, body.Base, bust.Base, bust.X);
				var yAddr = MemoryManager.GetAddressString(baseAddr, body.Base, bust.Base, bust.Y);
				var zAddr = MemoryManager.GetAddressString(baseAddr, body.Base, bust.Base, bust.Z);

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
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.ToString(), "Oh no!");
			}
		}
	}
}
