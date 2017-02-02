namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public class SharedCSharpLocCounter : CSharpLocCounter
	{
		public override IEnumerable<string> Categories => new[] { "Shared" }.Union(base.Categories);
	}
}
