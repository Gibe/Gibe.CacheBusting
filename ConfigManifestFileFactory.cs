using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gibe.FileSystem;
using Gibe.CacheBusting.Config;

namespace Gibe.CacheBusting
{
	public class ConfigManifestFileFactory : IManifestFileFactory
	{
		private readonly IFileService _fileService;
		private readonly IPaths _paths;

		public ConfigManifestFileFactory(IFileService fileService, IPaths paths)
		{
			_fileService = fileService;
			_paths = paths;
		}

		public IEnumerable<ManifestFile> GetManifestFiles()
		{
			var config = (CacheBustingSection)ConfigurationManager.GetSection("cacheBusting");
			var list = new List<ManifestFile>();
			foreach (ManifestElement manifest in config.Manifests)
			{
				list.Add(new ManifestFile(_fileService, manifest.Path, _paths.ServerMapPath(manifest.File)));
			}
			return list;
		}
	}
}
