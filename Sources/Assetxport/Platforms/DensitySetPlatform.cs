using System.Linq;
namespace Assetxport
{
	using System;
	using System.Collections.Generic;

	public abstract class DensitySetPlatform : IPlatform
	{
		#region Properties

		public string Name { get; }

		public Dictionary<string, double> Densities { get; }

		#endregion

		public DensitySetPlatform(string name, Dictionary<string, double> densities)
		{
			this.Name = name;
			this.Densities = densities;
		}

		public virtual void Export(IAsset asset, string folder)
		{
			foreach (var density in this.Densities.Where(x => x.Value <= asset.Density))
			{
				Log.Write($"Density '{density.Key}' = {density.Value}x");
				var assetPath = this.GetAssetPath(asset.FilenameWithoutQualifierAndExtension, asset.Extension, density.Key, density.Value);
				var path = System.IO.Path.Combine(folder, assetPath);
				asset.Export(path, density.Value);
			}
		}

		protected abstract string GetAssetPath(string name, string extension, string qualifier, double density);
	}
}
