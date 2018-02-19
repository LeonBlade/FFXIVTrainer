using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace FFXIVTrainer.ViewModels
{
	public class MainWindowViewModel
	{
		public BackgroundWorker worker;

		public MainWindowViewModel()
		{
			// open the process to FFXIV
			MemoryManager.Instance.MemLib.OpenProcess("ffxiv_dx11");

			// create an xml serializer
			var serializer = new XmlSerializer(typeof(Settings), "");
			// create namespace to remove it for the serializer
			var ns = new XmlSerializerNamespaces();
			// add blank namespaces
			ns.Add("", "");

			// using a stream reader
			using (var reader = new StreamReader(@"Settings.xml"))
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

			// create a background worker
			worker = new BackgroundWorker();
			// work loop for background worker
			worker.DoWork += Worker_DoWork;

			// start the work loop
			worker.RunWorkerAsync();
		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e)
		{
			// base address for the memory region
			string aobSearch = MemoryManager.Instance.GetBaseAddress(int.Parse(Settings.Instance.AoBOffset, System.Globalization.NumberStyles.HexNumber));
			// perform the scan and get the task
			var task = MemoryManager.Instance.MemLib.AoBScan(0, 0x7FFFFFFFFFFF, aobSearch);
			// get the base address
			MemoryManager.Instance.BaseAddress = task.Result.ToString("X");

			// dispose of the task now that we got the address
			task.Dispose();

			// just gonna trigger the GC because the memory is just stupid
			GC.Collect();

			while (true)
			{
				// sleep for 200 ms
				Thread.Sleep(200);

				// check if our memory manager is set
				if (!MemoryManager.Instance.IsReady())
					continue;

				// emit the worker loop
				Linkshell.Emit("WORKER");
			}
		}
	}
}
