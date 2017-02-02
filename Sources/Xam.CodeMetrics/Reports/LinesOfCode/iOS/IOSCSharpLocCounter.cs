namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public class IOSSharpLocCounter : CSharpLocCounter
	{
		public override IEnumerable<string> Categories => new[] { "iOS", "Platform" }.Union(base.Categories);

		public override bool IsValid(string path) => base.IsValid(path) && path.IsProjectType("iOS");
	}
}
