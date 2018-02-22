using System;
using System.ComponentModel;
using System.Text;

namespace FFXIVTrainer.Models
{
	public class BaseModel : INotifyPropertyChanged
	{
#pragma warning disable 67
		public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67
	}

	public class Address<T> : INotifyPropertyChanged
	{
		public string offset { get; set; }
		public T value { get; set; }
		public bool freeze { get; set; }

		/// <summary>
		/// Get a byte array of this address
		/// </summary>
		/// <returns></returns>
		public byte[] GetBytes()
		{
			try
			{
				return BitConverter.GetBytes((dynamic)value);
			}
			catch
			{
				return Encoding.UTF8.GetBytes((dynamic)value);
			}
		}

#pragma warning disable 67
		public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67
	}
}
