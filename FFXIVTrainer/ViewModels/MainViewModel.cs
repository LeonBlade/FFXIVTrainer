using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace FFXIVTrainer.ViewModels
{
	public class MainViewModel
	{
		private Mediator mediator;

		private BackgroundWorker worker;

		private CharacterViewModel character;
		private BustViewModel bust;
		private EntityListViewModel entityList;

		public MainViewModel()
		{
			// create a new instance of the mediator
			mediator = new Mediator();

			// open the process to FFXIV
			MemoryManager.Instance.MemLib.OpenProcess("ffxiv_dx11");

			// create an xml serializer
			var serializer = new XmlSerializer(typeof(Settings), "");
			// create namespace to remove it for the serializer
			var ns = new XmlSerializerNamespaces();
			// add blank namespaces
			ns.Add("", "");

			// using a stream reader
			using (var reader = new StreamReader(@"./Settings.xml"))
			{
				try
				{
					Settings.Instance = (Settings)serializer.Deserialize(reader);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}

			// initialize a background worker
			worker = new BackgroundWorker();
			worker.DoWork += Worker_DoWork;

			// run the worker
			worker.RunWorkerAsync();

			// create new instance of the character view model
			character = new CharacterViewModel(mediator);
		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e)
		{
			// base address for the memory region
			string baseAddr = MemoryManager.Instance.GetBaseAddress(int.Parse(Settings.Instance.AoBOffset, System.Globalization.NumberStyles.HexNumber));
			// perform the scan and get the task
			// var task = MemoryManager.Instance.MemLib.AoBScan(0, 0x7FFFFFFFFFFF, aobSearch);
			// get the base address
			//MemoryManager.Instance.BaseAddress = task.Result.ToString("X");

			// dispose of the task now that we got the address
			//task.Dispose();

			// just gonna trigger the GC because the memory is just stupid
			//GC.Collect();

			// no fancy tricks here boi
			MemoryManager.Instance.BaseAddress = baseAddr;

			while (true)
			{
				// sleep for 200 ms
				Thread.Sleep(200);

				// check if our memory manager is set
				if (!MemoryManager.Instance.IsReady())
					continue;

				// send out the work call to the mediator
				mediator.SendWork();
			}
		}

		public EntityListViewModel	EntityList		{ get => entityList;	set => entityList = value; }
		public CharacterViewModel	Character		{ get => character;		set => character =	value; }
		public BustViewModel		Bust			{ get => bust;			set => bust =		value; }
	}
}
