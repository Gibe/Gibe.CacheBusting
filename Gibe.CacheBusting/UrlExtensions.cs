using System.Web.Mvc;

namespace Gibe.CacheBusting
{
	public static class UrlExtensions
	{
		public static string Asset(this UrlHelper url, string filename)
		{
			var manifest = DependencyResolver.Current.GetService<IRevisionManifest>();
			return manifest.ContainsPath(filename) ? manifest.GetHashedPath(filename) : filename;
		}
	}
}
