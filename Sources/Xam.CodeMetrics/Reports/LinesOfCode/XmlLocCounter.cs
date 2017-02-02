namespace Xam.CodeMetrics
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class XmlLocCounter : LocCounter
	{
		public XmlLocCounter() : base("<!--.*?-->") { }

		public override IEnumerable<string> Categories => new string[0];

		public override IEnumerable<string> Extensions => new[] { ".xml" };

		protected override int CountCommentLines(string sources)
		{
			var matches = Regex.Matches(sources, $@"<!--((.|[\r\n])*?)-->", RegexOptions.Multiline);

			var result = 0;
			foreach (Match m in matches)
			{
				var content = m.Groups[1].Value;
				result += content.CountLines();
			}

			return base.CountCommentLines(sources) + result;
		}
	}
}
