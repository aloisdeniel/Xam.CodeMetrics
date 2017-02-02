namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public class UITestCSharpLocCounter : CSharpLocCounter
	{
		public override IEnumerable<string> Categories => new[] { "Test", "UITest" }.Union(base.Categories);

		public override bool IsValid(string path) => base.IsValid(path) && path.IsProjectType("UITests");
	}
}