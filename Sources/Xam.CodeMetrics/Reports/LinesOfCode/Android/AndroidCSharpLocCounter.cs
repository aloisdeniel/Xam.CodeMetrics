namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public class AndroidCSharpLocCounter : CSharpLocCounter
	{
		public override IEnumerable<string> Categories => new[] { "Android", "Platform" }.Union(base.Categories);

		public override bool IsValid(string path) => base.IsValid(path) 
		                                                 && path.IsProjectType("Droid");

		public override bool IsIgnored(string path)
		{
			return base.IsIgnored(path) || (System.IO.Path.GetFileName(path) == "Resource.designer.cs");
		}
	}
}
