namespace Assetxport
{
	using System.IO;
	using Newtonsoft.Json;

	public class Options
	{
		public string Platform { get; set; }

		public string Output { get; set; } = ".";

		public string[] Input { get; set; } = new string[0];

		public static Options Load(string path)
		{
			var json = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<Options>(json);
		}
	}
}
