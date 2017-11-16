namespace Assetxport.Cli
{
	using System.IO;
	using System.Linq;
	using Args;
	using System;
	using System.ComponentModel;
	using System.Collections.Generic;

	class MainClass
	{
		[ArgsModel(SwitchDelimiter = "-")]
		[Description("Assertxport")]
		public class ExportArgs
		{
			[Description("A path to an option configuration file.")]
			public string Options { get; set; }

			[Description("The path to a folder that contains all your assets.")]
			public string Input { get; set; }

			[Description("The path to the output folder that will contain all your exported assets.")]
			public string Output { get; set; }

			[Description("The target platform (iOS|Android|UWP)")]
			public string Platform { get; set; }
		}

		private static Options[] ParseOptions(ExportArgs args)
		{
			var result = new List<Options>();

			if (!string.IsNullOrEmpty(args.Options))
			{
				foreach (var file in args.Options.Split(';').Select(x => x.Trim()))
				{
					var options = Options.Load(file);
					var parent = Path.GetDirectoryName(file);
					options.Output = Path.Combine(parent, options.Output);
					options.Input = options.Input.Select(x => Path.Combine(parent, x)).ToArray();
					result.Add(options);
				}
			}

			if (!string.IsNullOrEmpty(args.Input) && !string.IsNullOrEmpty(args.Output) && !string.IsNullOrEmpty(args.Platform))
			{
				var options = new Options();
				options.Input = options.Input.Concat(new[] { args.Input }).ToArray();
				options.Output = args.Output;
				options.Platform = args.Platform;
				result.Add(options);
			}

			return result.ToArray();
		}

		public static void Main(string[] stringArgs)
		{
			Log.Write = (m) => Console.WriteLine(m);

			var args = Configuration.Configure<ExportArgs>().CreateAndBind(stringArgs);
			var options = ParseOptions(args);

			if(options.Length == 0)
			{
				Console.WriteLine("No assets to export.");
				return;
			}

			foreach (var option in options)
			{

				Console.WriteLine("Export starting...");
				Console.WriteLine($"Output: {option.Output}");
				foreach (var input in option.Input)
				{
					Console.WriteLine($"Input: {input}\n");
				}

				var exporter = new Exporter();
				exporter.Export(option);
				Console.WriteLine("Export succeeded.");
			}
		}
	}
}
