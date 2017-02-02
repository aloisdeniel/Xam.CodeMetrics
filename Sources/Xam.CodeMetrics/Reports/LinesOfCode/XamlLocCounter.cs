namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public abstract class XamlLocCounter : XmlLocCounter
	{
		public override IEnumerable<string> Categories => new[] { "Layout", "UI" }.Union(base.Categories);

		public override IEnumerable<string> Extensions => new[] { ".xaml" };
	}
}
