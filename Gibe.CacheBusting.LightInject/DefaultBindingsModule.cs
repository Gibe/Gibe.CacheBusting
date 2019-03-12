using LightInject;

namespace Gibe.CacheBusting.LightInject
{
	public static class DefaultBindingsModule
	{
		public static void RegisterServicesFactories(IServiceRegistry container)
		{
			container.Register<Gibe.CacheBusting.IRevisionManifest, Gibe.CacheBusting.RevisionManifest>();
			container.Register<Gibe.CacheBusting.IManifestFileFactory, Gibe.CacheBusting.ConfigManifestFileFactory>();
			container.Register<Gibe.FileSystem.IDirectoryService, Gibe.FileSystem.DirectoryService>();
			container.Register<Gibe.FileSystem.IFileService, Gibe.FileSystem.FileService>();
			container.Register<Gibe.FileSystem.IPaths, Gibe.FileSystem.Paths>();
		}
	}
}