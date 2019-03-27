using Gibe.CacheBusting.Config;
using Gibe.FileSystem;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
#if NETCORE
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

#endif

namespace Gibe.CacheBusting
{
	public class ConfigManifestFileFactory : IManifestFileFactory
	{
		private readonly IFileService _fileService;

#if NETFULL
		private readonly IPaths _paths;
#elif NETCORE
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IConfiguration _configuration;
#endif

#if NETFULL
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
				list.Add(new ManifestFile(_fileService, manifest.Path, MapPath(manifest.File)));
			}
			return list;
		}

		public string MapPath(string filename)
		{
			return _paths.ServerMapPath(filename);
		}
#elif NETCORE
		public ConfigManifestFileFactory(IFileService fileService, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
		{
			_fileService = fileService;
			_hostingEnvironment = hostingEnvironment;
			_configuration = configuration;
		}

		public IEnumerable<ManifestFile> GetManifestFiles()
		{
			var config = new CacheBustingSection(_configuration);
			var list = new List<ManifestFile>();
			foreach (var manifest in config.Manifests)
			{
				list.Add(new ManifestFile(_fileService, manifest.Path, MapPath(manifest.File)));
			}
			return list;
		}

		public string MapPath(string filename)
		{
			return Path.Join(_hostingEnvironment.WebRootPath, filename);
		}

#endif
	}
}
