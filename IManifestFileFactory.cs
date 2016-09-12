using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.CacheBusting
{
	public interface IManifestFileFactory
	{
		IEnumerable<ManifestFile> GetManifestFiles();
	}
}
