using System.Collections.Generic;

namespace Gibe.CacheBusting
{
	public interface IManifestFileFactory
	{
		IEnumerable<ManifestFile> GetManifestFiles();
	}
}
