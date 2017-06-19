using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;

namespace Gibe.CacheBusting
{
	public class DefaultNinjectBindingsModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IRevisionManifest>().To<RevisionManifest>().InSingletonScope();
			Bind<IManifestFileFactory>().To<ConfigManifestFileFactory>();
		}
	}
}
