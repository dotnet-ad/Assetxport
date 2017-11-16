namespace Assetxport
{
	using System.Collections.Generic;

	public class AndroidPlatform : DensitySetPlatform
	{
		private static readonly Dictionary<string, double> AndroidDensities = new Dictionary<string, double>
		{
			{ "ldpi",    0.75 },
			{ "mdpi",    1.00 },
			{ "hdpi",    1.50 },
			{ "xhdpi",   2.00 },
			{ "xxhdpi",  3.00 },
			{ "xxxhdpi", 4.00 },
		};

		public AndroidPlatform() : base("Android", AndroidDensities) {}

		protected override string GetAssetPath(string name, string extension, string qualifier, double density)
		{
			return System.IO.Path.Combine($"drawable-{qualifier}", $"{name}{extension}");
		}
	}
}
