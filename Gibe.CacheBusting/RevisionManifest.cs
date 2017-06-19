using System.Collections.Generic;
using System.Linq;

namespace Gibe.CacheBusting
{
	public class RevisionManifest : IRevisionManifest
	{
		private readonly IManifestFileFactory _manifestFileFactory;
		private Dictionary<string, string> _lookup;
		
		public RevisionManifest(IManifestFileFactory manifestFileFactory)
		{
			_manifestFileFactory = manifestFileFactory;
		}

		public bool ContainsPath(string path)
		{
			return Lookup().ContainsKey(path);
		}

		public string GetHashedPath(string original)
		{
			return Lookup()[original];
		}

		private Dictionary<string, string> Lookup()
		{
			if (_lookup == null)
			{
				_lookup = new Dictionary<string, string>();
				var files = _manifestFileFactory.GetManifestFiles().ToArray();
				foreach (var file in files)
				{
					foreach (var kvp in file.GetManifest())
					{
						_lookup.Add(kvp.Key, kvp.Value);
					}
				}
				WatchFilesForChanges(files);
			}
			return _lookup;
		}

		private void WatchFilesForChanges(IEnumerable<ManifestFile> files)
		{
			foreach (var file in files)
			{
				file.Changed += (s, e) => _lookup = null;
			}
		}
	}
}
