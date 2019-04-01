using System;
using System.Collections.Generic;
using System.IO;
using Gibe.FileSystem;
using Newtonsoft.Json;

namespace Gibe.CacheBusting
{
	public class ManifestFile
	{
		private readonly IFileService _fileService;
		private readonly string _path;
		private readonly string _sourceFile;

		public event EventHandler Changed;

		public ManifestFile(IFileService fileService, string path, string sourceFile)
		{
			_fileService = fileService;
			_path = path;
			_sourceFile = sourceFile;
			
			WatchManifestForChanges(sourceFile);
		}

		private void WatchManifestForChanges(string file)
		{
			var fsw = new FileSystemWatcher(Path.GetDirectoryName(file), Path.GetFileName(file))
			{
				NotifyFilter = NotifyFilters.LastWrite
			};
			fsw.Created += (sender, args) => OnChanged(args);
			fsw.Changed += (sender, args) => OnChanged(args);
			fsw.EnableRaisingEvents = true;
		}

		public Dictionary<string, string> GetManifest()
		{
			if (!_fileService.FileExists(_sourceFile))
			{
				return new Dictionary<string, string>();
			}

			var json = _fileService.ReadAllText(_sourceFile);
			var lookups = Deserialize(json);

			var output = new Dictionary<string, string>();
			foreach (var kvp in lookups)
			{
				output.Add($"{_path}{kvp.Key}", $"{_path}{kvp.Value}");
			}
			return output;
		}

		private void OnChanged(EventArgs e)
		{
			Changed?.Invoke(this, e);
		}

		private Dictionary<string, string> Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
		}

	}
}
