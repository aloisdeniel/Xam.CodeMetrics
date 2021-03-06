﻿namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;

	public class WindowsCSharpLocCounter : CSharpLocCounter
	{
		public override IEnumerable<string> Categories => new[] { "Windows", "Platform" }.Union(base.Categories);

		public override bool IsValid(string path) => base.IsValid(path) && path.IsProjectType("Windows");
	}
}
