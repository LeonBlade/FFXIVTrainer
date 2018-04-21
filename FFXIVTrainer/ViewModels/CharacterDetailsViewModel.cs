using FFXIVTrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVTrainer.ViewModels
{
    public class CharacterDetailsViewModel : BaseViewModel
    {
		private string eOffset = "8";

		public CharacterDetails CharacterDetails { get => (CharacterDetails)model; set => model = value; }

		public CharacterDetailsViewModel(Mediator mediator) :base(mediator)
		{
			model = new CharacterDetails();

			mediator.Work += Work;
			mediator.EntitySelection += offset => eOffset = offset;
		}

		private void Work()
		{
			try
			{
				var baseAddr = MemoryManager.Add(MemoryManager.Instance.BaseAddress, eOffset);
				var bodyBase = Settings.Instance.Character.Body.Base;

				// get the offsets
				var height = MemoryManager.GetAddressString(baseAddr, bodyBase, Settings.Instance.Character.Body.Height);
				var muscle = MemoryManager.GetAddressString(baseAddr, bodyBase, Settings.Instance.Character.Body.MuscleTone);
				var tailSize = MemoryManager.GetAddressString(baseAddr, bodyBase, Settings.Instance.Character.Body.TailSize);
				var tailType = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.TailType);
				var head = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Head);
				var hair = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Hair);
				var nose = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Nose);
				var lips = MemoryManager.GetAddressString(baseAddr, Settings.Instance.Character.Lips);

				var mem = MemoryManager.Instance.MemLib;

				// set get values
				if (CharacterDetails.Height.freeze) mem.writeBytes(height, CharacterDetails.Height.GetBytes());
				else CharacterDetails.Height.value = mem.readFloat(height);

				if (CharacterDetails.Muscle.freeze) mem.writeBytes(muscle, CharacterDetails.Muscle.GetBytes());
				else CharacterDetails.Muscle.value = mem.readFloat(muscle);

				if (CharacterDetails.TailSize.freeze) mem.writeBytes(tailSize, CharacterDetails.TailSize.GetBytes());
				else CharacterDetails.TailSize.value = mem.readFloat(tailSize);

				if (CharacterDetails.TailType.freeze) mem.writeBytes(tailType, CharacterDetails.TailType.GetBytes());
				else CharacterDetails.TailType.value = (byte)mem.readByte(tailType);

				if (CharacterDetails.Head.freeze)
					mem.writeBytes(head, CharacterDetails.Head.GetBytes());
				else CharacterDetails.Head.value = (byte)mem.readByte(head);

				if (CharacterDetails.Hair.freeze) mem.writeBytes(hair, CharacterDetails.Hair.GetBytes());
				else CharacterDetails.Hair.value = (byte)mem.readByte(hair);

				if (CharacterDetails.Nose.freeze) mem.writeBytes(nose, CharacterDetails.Nose.GetBytes());
				else CharacterDetails.Nose.value = (byte)mem.readByte(nose);

				if (CharacterDetails.Lips.freeze) mem.writeBytes(lips, CharacterDetails.Lips.GetBytes());
				else CharacterDetails.Lips.value = (byte)mem.readByte(lips);
			}
			catch (System.Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message, "Oh no!");
				mediator.Work -= Work;
			}
		}
	}
}
