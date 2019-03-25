using System.Web.Mvc;

namespace Gibe.CacheBusting
{
	public static class UrlExtensions
	{
		public static string Asset(this UrlHelper url, string filename)
		{
			return RevisionManifest.Current.ContainsPath(filename) ? RevisionManifest.Current.GetHashedPath(filename) : filename;
		}
	}
}
