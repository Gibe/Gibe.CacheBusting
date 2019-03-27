#if NETCORE
using System;
using Gibe.FileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Gibe.CacheBusting
{
	public static class NetCoreServicesExtensions
	{
		public static void AddCacheBusting(this IServiceCollection services)
		{
			services.AddSingleton<IRevisionManifest, RevisionManifest>();
			services.AddTransient<IManifestFileFactory, ConfigManifestFileFactory>();
			services.AddTransient<IFileService, FileService>();
		}
	}
}
#endif
