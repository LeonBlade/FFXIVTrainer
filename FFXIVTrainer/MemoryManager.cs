using System;
using System.Globalization;
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
			return (memLib.procs.MainModule.BaseAddress.ToInt64() + offset).ToString("X");
		}

		/// <summary>
		/// Returns if there is a process opened
		/// </summary>
		/// <returns></returns>
		public bool IsReady()
		{
			return memLib.procID != 0 && !memLib.procs.HasExited;
		}

		/// <summary>
		/// Adds two hex strings together
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string Add(string a, string b)
		{
			return (long.Parse(a, NumberStyles.HexNumber) + long.Parse(b, NumberStyles.HexNumber)).ToString("X");
		}

		public static string GetAddressString(params string[] addr)
		{
			var ret = "";

			foreach (var a in addr)
				ret += a + ",";

			return ret.TrimEnd(',');
		}
	}
}
