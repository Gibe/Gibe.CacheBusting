using System.Collections.Generic;
using Gibe.FileSystem;

namespace Gibe.CacheBusting
{
	public class HardcodedManifestFileFactory : IManifestFileFactory
	{
		private readonly IFileService _fileService;
		private readonly IPaths _paths;

		public HardcodedManifestFileFactory(IFileService fileService, IPaths paths)
		{
			_fileService = fileService;
			_paths = paths;
		}

		public IEnumerable<ManifestFile> GetManifestFiles()
		{
			return new List<ManifestFile>
			{
				new ManifestFile(_fileService, "/css/", _paths.ServerMapPath("~/css/rev-manifest.json")),
				new ManifestFile(_fileService, "/js/", _paths.ServerMapPath("~/js/rev-manifest.json"))
			};
		}
	}
}
