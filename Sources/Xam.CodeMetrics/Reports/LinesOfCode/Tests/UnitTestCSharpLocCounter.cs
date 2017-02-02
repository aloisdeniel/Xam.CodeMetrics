namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public class UnitTestCSharpLocCounter : CSharpLocCounter
	{
		public override IEnumerable<string> Categories => new[] { "Test", "UnitTest" }.Union(base.Categories);

		public override bool IsValid(string path) => base.IsValid(path) && path.IsProjectType("Tests");
	}
}