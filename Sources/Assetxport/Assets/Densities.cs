namespace Assetxport
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	public static class Densities
	{
		public static readonly Regex ExplicitNaming = new Regex(@"\@([0-9]+(.[0-9]+)?)x\.[a-zA-Z]+$");

		public static readonly Regex QualifiedNaming = new Regex(@"\@([a-z]+)\.[a-zA-Z]+$");

		public static readonly Dictionary<string, double> KnownQualifiedNames = new Dictionary<string, double>
		{
			{ "ldpi",    0.75 },
			{ "mdpi",    1.00 },
			{ "hdpi",    1.50 },
			{ "xhdpi",   2.00 },
			{ "xxhdpi",  3.00 },
			{ "xxxhdpi", 4.00 },
		};

		public static bool TryFind(string path, out double density)
		{
			var expl = ExplicitNaming.Match(path);
			if (expl.Success)
			{
				density = double.Parse(expl.Groups[1].Value);
				return true;
			}

			var qualified = QualifiedNaming.Match(path);
			if (qualified.Success && KnownQualifiedNames.TryGetValue(expl.Groups[1].Value, out density))
			{
				return true;
			}

			density = 1.0;
			return false;
		}
	}
}
