using FFXIVTrainer.Models;
using System;

namespace FFXIVTrainer.ViewModels
{
    public class PositionViewModel : BaseViewModel
    {
		public Position Position { get => (Position)model; set => model = value; }
		public string eOffset = "8";

		public PositionViewModel(Mediator mediator) :base(mediator)
		{
			model = new Position();

			mediator.Work += Work;
			mediator.EntitySelection += offset => eOffset = offset;
		}

		private void Work()
		{
			try
			{
				var baseAddress = MemoryManager.Add(MemoryManager.Instance.BaseAddress, eOffset);
				var bodyOffset = Settings.Instance.Character.Body.Base;

				var x = MemoryManager.GetAddressString(baseAddress, bodyOffset, Settings.Instance.Character.Body.Position.X);
				var y = MemoryManager.GetAddressString(baseAddress, bodyOffset, Settings.Instance.Character.Body.Position.Y);
				var z = MemoryManager.GetAddressString(baseAddress, bodyOffset, Settings.Instance.Character.Body.Position.Z);
				var rotation = MemoryManager.GetAddressString(baseAddress, bodyOffset, Settings.Instance.Character.Body.Position.Rotation);

				var mem = MemoryManager.Instance.MemLib;

				if (Position.X.freeze)
					mem.writeBytes(x, Position.X.GetBytes());
				else
					Position.X.value = mem.readFloat(x);

				if (Position.Y.freeze)
					mem.writeBytes(y, Position.Y.GetBytes());
				else
					Position.Y.value = mem.readFloat(y);

				if (Position.Z.freeze)
					mem.writeBytes(z, Position.Z.GetBytes());
				else
					Position.Z.value = mem.readFloat(z);

				if (Position.Rotation.freeze)
					mem.writeBytes(rotation, Position.Rotation.GetBytes());
				else
					Position.Rotation.value = mem.readFloat(rotation);
			}
			catch (System.Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message, "Oh no!");
				System.Windows.MessageBox.Show("Disabling " + this.GetType().Name);
				mediator.Work -= Work;
			}
		}
	}
}
