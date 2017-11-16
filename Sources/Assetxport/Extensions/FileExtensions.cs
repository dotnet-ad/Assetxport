namespace Assetxport
{
	using System.IO;

	public static class FileExtensions
	{
		public static void CreateParentDirectory(this string path)
		{
			var parent = Path.GetDirectoryName(path);
			if (!string.IsNullOrEmpty(parent) && !Directory.Exists(parent))
				Directory.CreateDirectory(parent);
		}
	}
}
