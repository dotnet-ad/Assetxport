namespace Assetxport.Sample.Cli
{
	using System.IO;
	using System.Reflection;

	class Program
	{
		static void Main(string[] args)
		{
			var root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

			using (var asset = new Asset(Path.Combine(root, "Images/ic_warning@3x.png")))
			{
				asset.Export(Path.Combine(root, "Output/output.png"), 100, 100);
			}

			var exporter = new Exporter();

			exporter.Export(new Options
			{
				Input = new[] { Path.Combine(root, "Images/") },
				Output = Path.Combine(root, "Output/iOS/"),
				Platform = "iOS"
			});

			exporter.Export(new Options
			{
				Input = new[] { Path.Combine(root, "Images/") },
				Output = Path.Combine(root, "Output/Android/"),
				Platform = "Android"
			});
		}
	}
}
