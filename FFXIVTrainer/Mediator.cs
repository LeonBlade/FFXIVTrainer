namespace FFXIVTrainer
{
	public delegate void EntitySelectionEventHandler(string offset);
	public delegate void WorkEventHandler();

	public class Mediator
	{
		public event EntitySelectionEventHandler EntitySelection;
		public event WorkEventHandler Work;

		public void SendEntitySelection(string offset)
		{
			EntitySelection?.Invoke(offset);
		}

		public void SendWork()
		{
			Work?.Invoke();
		}
	}
}
