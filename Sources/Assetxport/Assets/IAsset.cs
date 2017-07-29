namespace Assetxport
{
	using System;

	public interface IAsset : IDisposable
	{
		string Path { get; }

		string FilenameWithoutQualifierAndExtension { get; }

		string Extension { get; }

		double Density { get; set; }

		void Export(string path, double density);

		void Export(string path, int width, int height);

	}
}    
