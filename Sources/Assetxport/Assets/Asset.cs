namespace Assetxport
{
	using System;
	using System.IO;
	using SkiaSharp;

	public class Asset : IAsset
	{
		#region Constructors

		public Asset(string path)
		{
			this.Path = path;
			this.Path.WithoutQualifier(out string name, out string extension);
			this.FilenameWithoutQualifierAndExtension = name;
			this.Extension = extension;

			if(Densities.TryFind(path, out double foundDensity))
			{
				this.Density = foundDensity;
			}

			using (var input = File.Open(path, FileMode.Open))
            using(var memory = new MemoryStream())
            {
                input.CopyTo(memory);
                memory.Seek(0, SeekOrigin.Begin);
                memory.Position = 0;

                using (var stream = new SKManagedStream(memory))
                {
                    this.bitmap = SKBitmap.Decode(stream);

                    if (this.bitmap == null)
                        throw new InvalidOperationException($"The provided image isn't valid : {path} (SKBitmap can't be created).");
                } 
            }
		}

		#endregion

		#region Fields

		private SKBitmap bitmap;

		#endregion

		#region Properties

		public string Path { get; }

		public string FilenameWithoutQualifierAndExtension { get; }

		public string Extension { get; }

		public double Density { get; set; } = 1.0;

		#endregion

		public void Export(string path, int width, int height)
		{
			if(!File.Exists(path))
			{
				Log.Write($"[{this.Path} ({this.bitmap.Width}x{this.bitmap.Height})({this.Density}x)] -> Generating [{path} ({width}x{height})]");

                // SKBitmap.Resize() doesn't support SKColorType.Index8
                // https://github.com/mono/SkiaSharp/issues/331
                if (bitmap.ColorType != SKColorType.Rgba8888)
                {
                    bitmap.CopyTo(bitmap, SKColorType.Rgba8888);
                }

                var info = new SKImageInfo(width, height);

				using (var resized = bitmap.Resize(info, SKBitmapResizeMethod.Lanczos3))
                {
                    if (resized == null)
                        throw new InvalidOperationException($"Failed to resize : {Path}.");

                    using (var image = SKImage.FromBitmap(resized))
                    using (var data = image.Encode())
                    {
                        path.CreateParentDirectory();

                        if (File.Exists(path))
                            File.Delete(path);

                        using (var fileStream = File.Create(path))
                        using (var outputStream = data.AsStream())
                        {
                            outputStream.CopyTo(fileStream);
                        }
                    }
                }
			}
			else
			{
				Log.Write($"[{this.Path} ({this.bitmap.Width}x{this.bitmap.Height})({this.Density}x)] -> Didn't generate [{path} ({width}x{height})] because it already exists.");

			}
		}

		public void Export(string path, double density)
		{
			var densityFactor = (density / this.Density);
			var width = (int)Math.Ceiling(this.bitmap.Width * densityFactor);
			var height = (int)Math.Ceiling(this.bitmap.Height * densityFactor);
			this.Export(path, width, height);
		}

		public void Dispose()
		{
			this.bitmap.Dispose();
		}
	}
}
