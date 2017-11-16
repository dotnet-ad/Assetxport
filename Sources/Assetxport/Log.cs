namespace Assetxport
{
	using System;
	using System.Diagnostics;

	public static class Log
	{
		public static Action<string> Write { get; set; } = (m) => Debug.WriteLine(m);
	}
}
