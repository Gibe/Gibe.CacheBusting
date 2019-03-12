using Ninject.Modules;

namespace Gibe.CacheBusting.Ninject
{
	public class DefaultBindingsModule : NinjectModule
	{
		public override void Load()
		{
			Bind<Gibe.CacheBusting.IRevisionManifest>().To<Gibe.CacheBusting.RevisionManifest>().InSingletonScope();
			Bind<Gibe.CacheBusting.IManifestFileFactory>().To<Gibe.CacheBusting.ConfigManifestFileFactory>();
			Bind<Gibe.CacheBusting.IManifestFileFactory>().To<Gibe.CacheBusting.ConfigManifestFileFactory>();
			Bind<Gibe.FileSystem.IDirectoryService>().To<Gibe.FileSystem.DirectoryService>();
			Bind<Gibe.FileSystem.IFileService>().To<Gibe.FileSystem.FileService>();
			Bind<Gibe.FileSystem.IPaths>().To<Gibe.FileSystem.Paths>();
		}
	}
}