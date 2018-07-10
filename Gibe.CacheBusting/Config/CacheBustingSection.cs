using System.Configuration;

namespace Gibe.CacheBusting.Config
{
	
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
			get { return (string) this["path"]; }
			set { this["path"] = value; }
		}

		[ConfigurationProperty("file", IsRequired = true, IsKey = true)]
		public string File
		{
			get { return (string)this["file"]; }
			set { this["file"] = value; }
		}
	}
}
