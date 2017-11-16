namespace Assetxport
{
	using System;
	using System.Linq;

	public static class QualifierExtensions
	{
		public static void WithoutQualifier(this string path, out string name, out string extension)
		{
			name = System.IO.Path.GetFileNameWithoutExtension(path);
			extension = System.IO.Path.GetExtension(path);

			var splits = name.Split('@');
			if (splits.Length > 1)
			{
				name = string.Join("@", splits.Take(splits.Length - 1));
			}
		}
	}
}
