using Autofac;

namespace Gibe.CacheBusting.Autofac
{
	public class DefaultBindingsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<Gibe.CacheBusting.RevisionManifest>().As<Gibe.CacheBusting.IRevisionManifest>();
			builder.RegisterType<Gibe.CacheBusting.ConfigManifestFileFactory>().As<Gibe.CacheBusting.IManifestFileFactory>();
			builder.RegisterType<Gibe.FileSystem.DirectoryService>().As<Gibe.FileSystem.IDirectoryService>();
			builder.RegisterType<Gibe.FileSystem.FileService>().As<Gibe.FileSystem.IFileService>();
			builder.RegisterType<Gibe.FileSystem.Paths>().As<Gibe.FileSystem.IPaths>();
		}
	}
}