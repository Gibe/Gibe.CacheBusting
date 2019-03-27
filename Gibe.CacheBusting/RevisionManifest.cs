using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.FileSystem;

namespace Gibe.CacheBusting
{
	public class RevisionManifest : IRevisionManifest
	{
		// Singleton
		private static IRevisionManifest _revisionManifest;

		private readonly IManifestFileFactory _manifestFileFactory;
		private Dictionary<string, string> _lookup;
		
		internal RevisionManifest(IManifestFileFactory manifestFileFactory)
		{
			_manifestFileFactory = manifestFileFactory;
		}

		public static IRevisionManifest Current
		{
			get
			{
				if (_revisionManifest == null)
				{
#if NETFULL
					_revisionManifest = new RevisionManifest(new ConfigManifestFileFactory(new FileService(), new Paths()));
#elif NETCORE
					throw new NotSupportedException("CacheBusting has not been configured at startup (Hint: Add app.UseCacheBusting() to Configure)");
#endif
				}
				return _revisionManifest;
			}
			internal set { _revisionManifest = value; }
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
