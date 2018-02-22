using FFXIVTrainer.Models;

namespace FFXIVTrainer.ViewModels
{
    public class ScaleViewModel : BaseViewModel
    {
		public Scale Scale { get => (Scale)model; }
		private string eOffset = "8";

		public ScaleViewModel(Mediator mediator) : base(mediator)
		{
			// initialize a new scale
			model = new Scale();

			// updating the selected entity offset
			mediator.EntitySelection += offset => eOffset = offset;

			// work loop
			mediator.Work += Work;
		}

		private void Work()
		{
			try
			{
				// the base address for the current entity
				var baseAddr = MemoryManager.Add(MemoryManager.Instance.BaseAddress, eOffset);
				var bodyBase = Settings.Instance.Character.Body.Base;

				// get the addresses for this loop
				var xAddr = MemoryManager.GetAddressString(baseAddr, bodyBase, Settings.Instance.Character.Body.Scale.X);
				var yAddr = MemoryManager.GetAddressString(baseAddr, bodyBase, Settings.Instance.Character.Body.Scale.Y);
				var zAddr = MemoryManager.GetAddressString(baseAddr, bodyBase, Settings.Instance.Character.Body.Scale.Z);

				// store reference to memlib
				var mem = MemoryManager.Instance.MemLib;

				if (Scale.ScaleX.freeze)
					mem.writeBytes(xAddr, Scale.ScaleX.GetBytes());
				else
					Scale.ScaleX.value = mem.readFloat(xAddr);

				if (Scale.ScaleY.freeze)
					mem.writeBytes(yAddr, Scale.ScaleY.GetBytes());
				else
					Scale.ScaleY.value = mem.readFloat(yAddr);

				if (Scale.ScaleZ.freeze)
					mem.writeBytes(zAddr, Scale.ScaleZ.GetBytes());
				else
					Scale.ScaleZ.value = mem.readFloat(zAddr);
			}
			catch (System.Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message, "Oh no!");
				System.Windows.MessageBox.Show("Disabling " + (this).GetType().Name);
				mediator.Work -= Work;
			}
		}
	}
}
