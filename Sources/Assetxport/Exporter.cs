namespace Assetxport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.IO;

	public class Exporter
	{
		public Exporter()
		{
			this.Register(new iOSPlatform());
			this.Register(new AndroidPlatform());
			this.Register(new UwpPlatform());
		}

		private List<IPlatform> platforms = new List<IPlatform>();

		public string[] SupportedExtensions { get; set; } = new[] { ".png", ".jpg" };

		public Exporter Register(IPlatform platform)
		{
			this.platforms.Add(platform);
			return this;
		}

		public void Export(Options configuration)
		{
			var platform = this.platforms.FirstOrDefault(x => x.Name == configuration.Platform);

			if (platform == null)
				throw new NullReferenceException("Platform not found");

			var assetPaths = configuration.Input.SelectMany(x => Directory.GetFiles(x)).Where(x => SupportedExtensions.Contains(Path.GetExtension(x.ToLower())));
			foreach (var path in assetPaths)
			{
				using (var asset = new Asset(path))
				{
					platform.Export(asset, configuration.Output);
				}
			}
		}
	}
}
