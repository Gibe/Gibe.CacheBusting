using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Gibe.FileSystem;

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
			_path = path;
			_sourceFile = sourceFile;
			_fileService = fileService;

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
			var lookups = Json.Decode<Dictionary<string, string>>(json);

			var output = new Dictionary<string, string>();
			foreach (var kvp in lookups)
			{
				output.Add($"{_path}{kvp.Key}",$"{_path}{kvp.Value}");
			}
			return output;
		}

		public void OnChanged(EventArgs e)
		{
			Changed?.Invoke(this, e);
		}
	}
}
