namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class CSharpLocCounter : LocCounter
	{
		public CSharpLocCounter() : base("/\\*(.*?)\\*/") { }

		public override IEnumerable<string> Categories => new string [0];

		public override IEnumerable<string> Extensions => new [] { ".cs" };

		public override bool IsIgnored(string path)
		{
			return base.IsIgnored(path) || Path.GetFileName(path) == "AssemblyInfo.cs";
	 	}

		private const string IgnoredLines = @"^((using\s*[A-Za-z])|(//)|([\{\}]\s*)+)";

		protected override bool IgnoreLine(string line)
		{
			return base.IgnoreLine(line) || Regex.Match(line, IgnoredLines, RegexOptions.None).Success;   
		}

		protected override int CountCommentLines(string sources)
		{
			const string strings = @"""((\\[^\n]|[^""\n])*)""";
			const string verbatimStrings = @"@(""[^""]*"")+";

			sources = Regex.Replace(sources, $"{strings}|{verbatimStrings}", string.Empty);

			var matches = Regex.Matches(sources, $@"(\/\*((.|[\r\n])*?)\*\/)|(\/\/(.*?)\n)", RegexOptions.Multiline);

			var result = 0;
			foreach (Match m in matches)
			{
				var content = m.Value.StartsWith("/*") ? m.Groups[2].Value : m.Groups[5].Value;
				result += content.CountLines();
			}

			return base.CountCommentLines(sources) + result;
		}
	}
}
