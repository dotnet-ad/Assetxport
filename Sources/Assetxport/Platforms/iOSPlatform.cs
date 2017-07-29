namespace Assetxport
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class iOSPlatform : DensitySetPlatform
	{
		private static readonly Dictionary<string, double> iOSDensities = new Dictionary<string, double>
		{
			{ "",       1},
			{ "@2x",    2},
			{ "@3x",    3},
		};

		public iOSPlatform() : base("iOS", iOSDensities) { }

		#region Contents templates

		private const string ContentsItemTemplate = @"{{ ""filename"": ""{0}"", ""scale"": ""{1}x"", ""idiom"": ""universal"" }}";

		private const string ContentsTemplate = @"{{ ""images"": [ {0} ], ""info"": {{ ""version"": 1, ""author"": ""xcode"" }} }}";

		private void GenerateContents(IAsset asset, string folder)
		{
			var imagesetFolder = Path.Combine(folder, $"{asset.FilenameWithoutQualifierAndExtension}.imageset");

			// Writing Contents.json
			var contentFile = Path.Combine(imagesetFolder, $"Contents.json");
			var densities = Densities.Where(x => x.Value <= asset.Density);
			var images = string.Join(", ", densities.Select(d => string.Format(ContentsItemTemplate, CreateAssetFilename(asset.FilenameWithoutQualifierAndExtension, asset.Extension, d.Key, d.Value), d.Value.ToString("0.##"))));
			contentFile.CreateParentDirectory();
			File.WriteAllText(contentFile, string.Format(ContentsTemplate, images));
		}

		#endregion

		public override void Export(IAsset asset, string folder)
		{
			base.Export(asset, folder);
			this.GenerateContents(asset, folder);
		}

		protected override string GetAssetPath(string name, string extension, string qualifier, double density)
		{
			var imagesetFolder = $"{name}.imageset";
			var imageFile = CreateAssetFilename(name, extension, qualifier, density);
			return Path.Combine(imagesetFolder, $"{name}{qualifier}{extension}");
		}

		private string CreateAssetFilename(string name, string extension, string qualifier, double density)
		{
			return $"{name}{qualifier}{extension}";
		}
	}
}
