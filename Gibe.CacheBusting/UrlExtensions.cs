#if NETFULL
using System.Web.Mvc;
#elif NETCORE
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
#endif

namespace Gibe.CacheBusting
{
	public static class UrlExtensions
	{
#if NETFULL
		public static string Asset(this UrlHelper url, string filename)
		{
			return RevisionManifest.Current.ContainsPath(filename) ? RevisionManifest.Current.GetHashedPath(filename) : filename;
		}
#elif NETCORE
		public static string Asset(this IUrlHelper url, string filename)
		{
			var revisionManifest = (IRevisionManifest)url.ActionContext.HttpContext.RequestServices.GetService(typeof(IRevisionManifest));
			return revisionManifest.ContainsPath(filename) ? revisionManifest.GetHashedPath(filename) : filename;
		}

#endif
	}
}
