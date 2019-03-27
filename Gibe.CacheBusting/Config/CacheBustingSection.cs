﻿using System.Collections.Generic;
#if NETFULL
using System.Configuration;
#elif NETCORE
using Microsoft.Extensions.Configuration;
#endif

namespace Gibe.CacheBusting.Config
{
#if NETFULL
	public class CacheBustingSection : ConfigurationSection
	{
		[ConfigurationProperty("manifests", IsRequired = true)]
		public ManifestElementCollection Manifests => (ManifestElementCollection) base["manifests"];
	}

	[ConfigurationCollection(typeof(ManifestElementCollection), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class ManifestElementCollection : ConfigurationElementCollection
	{
		public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;

		protected override ConfigurationElement CreateNewElement()
		{
			return new ManifestElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ManifestElement) element).File;
		}
	}

	public class ManifestElement : ConfigurationElement
	{
		[ConfigurationProperty("path", IsRequired = true, IsKey = false)]
		public string Path
		{
			get => (string) this["path"];
			set => this["path"] = value;
		}

		[ConfigurationProperty("file", IsRequired = true, IsKey = true)]
		public string File
		{
			get => (string)this["file"];
			set => this["file"] = value;
		}
	}
#elif NETCORE
	public class CacheBustingSection
	{
		private readonly IConfiguration _configuration;

		public CacheBustingSection(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public IEnumerable<Manifest> Manifests
		{
			get
			{
				foreach (var section in _configuration.GetSection("cacheBusting").GetChildren())
				{
					yield return new Manifest {Path = section.Key, File = section.Value};
				}
			}
		}
	}

	public class Manifest
	{
		public string Path { get; set; }
		public string File { get; set; }
	}
#endif
}
