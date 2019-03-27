#if NETCORE
using Gibe.CacheBusting;
using Gibe.FileSystem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gibe.CacheBusting2
{

	public static class NetCoreServicesExtensions
	{
		public static void AddCacheBusting(this IApplicationBuilder app)
		{
			RevisionManifest.Current = new RevisionManifest(
				new ConfigManifestFileFactory(
					new FileService(), 
					app.ApplicationServices.GetService<IHostingEnvironment>(), 
					app.ApplicationServices.GetService<IConfiguration>()));
		}
	}
}
#endif
