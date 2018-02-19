using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVTrainer.Models
{
	public class BaseModel : INotifyPropertyChanged
	{
#pragma warning disable 67
		public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67


		public string[] Settings { get; set; }
	}

	public class Address<T> : INotifyPropertyChanged
	{
		public string offset { get; set; }
		public T value { get; set; }
		public bool freeze { get; set; }

		public Address()
		{

		}

		/// <summary>
		/// Get a byte array of this address
		/// </summary>
		/// <returns></returns>
		public byte[] GetBytes()
		{
			var bf = new BinaryFormatter();
			using (var ms = new MemoryStream())
			{
				bf.Serialize(ms, value);
				return ms.ToArray();
			}
		}

#pragma warning disable 67
		public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67
	}
}
