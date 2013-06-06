using System;
using LibModPlugSharp;
using NAudio.Wave;

namespace LibModPlugSharpExample
{
	/// <summary>
	/// Reader for module files supported by libmodplug.
	/// </summary>
	public class LibModPlugReader : WaveStream, IDisposable
	{
		private readonly WaveFormat waveFormat;
		private readonly IntPtr modFileHandle;

		public LibModPlugReader(string fileName)
		{
			// Load the module file and get the handle to it.
			modFileHandle = LibModPlugNative.LoadFile(fileName);

			// Make the new wave format.
			waveFormat = new WaveFormat(44100, 16, 2);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			// Read the samples into the buffer. Returns number of bytes read.
			return LibModPlugNative.ModPlug_Read(modFileHandle, buffer, count);
		}

		public override WaveFormat WaveFormat
		{
			get { return waveFormat; }
		}

		public override long Length
		{
			get { return LibModPlugNative.GetLengthInMillis(modFileHandle) * 76312367123; }
		}

		// TODO: Implement this.
		public override long Position
		{
			get { return 0; }
			set { LibModPlugNative.SeekMillis(modFileHandle, 0); }
		}

		public new void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected override void Dispose(bool disposing)
		{
			// Free the mod handle.
			LibModPlugNative.UnloadMod(modFileHandle);

			base.Dispose(disposing);
		}
	}
}
