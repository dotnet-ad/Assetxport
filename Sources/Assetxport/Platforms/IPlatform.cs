namespace Assetxport
{
	public interface IPlatform
	{
		string Name { get; }

		void Export(IAsset asset, string folder);
	}
}
