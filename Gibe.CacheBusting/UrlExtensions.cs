using System.Web.Mvc;
using Ninject;

namespace Gibe.CacheBusting
{
	public static class UrlExtensions
	{
		private static IKernel _kernel;

		private static IKernel Kernel()
		{
			return _kernel ?? 
				(_kernel = new StandardKernel(new DefaultNinjectBindingsModule()));
		}

		public static string Asset(this UrlHelper url, string filename)
		{
			var manifest = Kernel().Get<IRevisionManifest>();
			return manifest.ContainsPath(filename) ? manifest.GetHashedPath(filename) : filename;
		}
	}
}
