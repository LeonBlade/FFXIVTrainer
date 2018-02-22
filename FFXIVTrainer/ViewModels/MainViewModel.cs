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
		private ScaleViewModel scale;
		private PositionViewModel position;

		public EntityListViewModel EntityList	{ get => entityList;	set => entityList	= value; }
		public CharacterViewModel Character		{ get => character;		set => character	= value; }
		public BustViewModel Bust				{ get => bust;			set => bust			= value; }
		public ScaleViewModel Scale				{ get => scale;			set => scale		= value; }
		public PositionViewModel Position		{ get => position;		set => position		= value; }

		public MainViewModel()
		{
			// create a new instance of the mediator
			mediator = new Mediator();

			// open the process to FFXIV
			MemoryManager.Instance.MemLib.OpenProcess("ffxiv_dx11");

			// load the settings
			LoadSettings();

			// initialize a background worker
			worker = new BackgroundWorker();
			worker.DoWork += Worker_DoWork;

			// run the worker
			worker.RunWorkerAsync();

			// create the view model instances
			character = new CharacterViewModel(mediator);
			bust = new BustViewModel(mediator);
			entityList = new EntityListViewModel(mediator);
			scale = new ScaleViewModel(mediator);
			position = new PositionViewModel(mediator);
		}

		private void LoadSettings()
		{
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
		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e)
		{
			// base address for the memory region
			string baseAddr = MemoryManager.Instance.GetBaseAddress(int.Parse(Settings.Instance.AoBOffset, System.Globalization.NumberStyles.HexNumber));

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
	}
}
