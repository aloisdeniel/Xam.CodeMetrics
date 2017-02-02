namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public class AndroidXmlLocCounter : XmlLocCounter
	{
		public AndroidXmlLocCounter() { }

		public override IEnumerable<string> Categories => new[] { "Layout", "UI", "Android", "Platform" }.Union(base.Categories);

		public override IEnumerable<string> Extensions => new[] { ".xml", ".axml" };

		public override bool IsValid(string path) => base.IsValid(path) && path.IsProjectType("Droid");

		public override bool IsIgnored(string path)
		{
			var filename = System.IO.Path.GetFileName(path);
			return base.IsIgnored(path) || filename == "AndroidManifest.xml";
		}
	}
}
