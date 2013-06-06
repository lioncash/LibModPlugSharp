using System;
using NAudio.Wave;

namespace LibModPlugSharpExample
{
	/// <summary>
	/// Example use of LibModPlugSharp with NAudio.
	/// </summary>
	internal class Program
	{
		internal static void Main()
		{
			// Module to open.
			const string modFile = "Jakim - Love in the air.xm";

			// Initialize the reader.
			LibModPlugReader modReader = new LibModPlugReader(modFile);
			IWavePlayer player = new WaveOut();
			player.Init(modReader);
			player.Play();

			Console.Write("Press any key to exit.");

			// Keep console window up.
			Console.ReadKey();
		}
	}
}
