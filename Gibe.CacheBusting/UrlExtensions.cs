#if NETFULL
using System.Web.Mvc;
#elif NETCORE
using Microsoft.AspNetCore.Mvc.Routing;
#endif

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
