using System;
using System.Text;
using Memory;

namespace FFXIVTrainer
{
	public class MemoryManager
	{
		#region Singleton

		private static MemoryManager instance;
		/// <summary>
		/// Singleton instance of the MemoryManager
		/// </summary>
		public static MemoryManager Instance
		{
			get
			{
				// create an instance of the MemoryManager if the value is null
				if (instance == null)
					instance = new MemoryManager();
				return instance;
			}
		}

		#endregion

		/// <summary>
		/// The mem instance
		/// </summary>
		private Mem memLib;
		public Mem MemLib
		{
			get => memLib;
		}

		public string BaseAddress { get; set; }

		/// <summary>
		/// Constructor for the singleton memory manager
		/// </summary>
		public MemoryManager()
		{
			// create a new instance of Mem
			memLib = new Mem();
		}

		/// <summary>
		/// Open the process in MemLib
		/// </summary>
		/// <param name="pid"></param>
		public void OpenProcess(int pid)
		{
			// open the process
			if (!memLib.OpenProcess(pid.ToString()))
				throw new Exception("Couldn't open process!");
		}

		/// <summary>
		/// Get a string for use in memlib
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public string GetBaseAddress(long offset)
		{
			// get the value for the AoB Search
			long aobSearch = memLib.procs.MainModule.BaseAddress.ToInt64() + offset;
			// get byte array of this address
			var bytes = BitConverter.GetBytes(aobSearch);
			// create a string for this search
			var sb = new StringBuilder(bytes.Length * 2);
			// loop over bytes and append to the string with hex values
			foreach (var b in bytes)
				sb.AppendFormat("{0:x2} ", b);
			// return the string
			return sb.ToString().TrimEnd();
		}

		/// <summary>
		/// Returns if there is a process opened
		/// </summary>
		/// <returns></returns>
		public bool IsReady()
		{
			return memLib.procID != 0;
		}

		public static string GetAddressString(params string[] addr)
		{
			var ret = "";

			foreach (var a in addr)
				ret += "0x" + a + ",";

			return ret.TrimEnd(',');
		}
	}
}
