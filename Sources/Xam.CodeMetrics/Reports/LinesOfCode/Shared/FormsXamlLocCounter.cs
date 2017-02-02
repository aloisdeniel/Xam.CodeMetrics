namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public class FormsXamlLocCounter : XamlLocCounter
	{
		public override IEnumerable<string> Categories => new[] { "Shared" }.Union(base.Categories);
	}
}
