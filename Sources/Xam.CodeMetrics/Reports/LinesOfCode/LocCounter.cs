using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Xam.CodeMetrics
{
	public abstract class LocCounter
	{
		public LocCounter(string removeRegex)
		{
			this.RemoveRegex = removeRegex;
		}

		public abstract IEnumerable<string> Extensions { get; }

		public abstract IEnumerable<string> Categories { get;  }

		public string RemoveRegex { get; }

		public virtual bool IsValid(string path) => Extensions.Select(x =>  x.ToLowerInvariant()).Contains(Path.GetExtension(path.ToLowerInvariant()));

		public virtual bool IsIgnored(string path) => false;

		protected virtual bool IgnoreLine(string line) => string.IsNullOrWhiteSpace(line);

		protected virtual int CountCommentLines(string sources) => 0;

		public Loc Count(string path)
		{
			if (this.IsIgnored(path))
				return null;
			
			var sources = File.ReadAllText(path);
			IEnumerable<string> lines = Regex.Replace(sources, RemoveRegex, string.Empty).Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);;
			lines = lines.Select(x => x.Trim()).Where(l => !IgnoreLine(l));
			return new Loc(path, lines.Count(), CountCommentLines(sources), this.Categories.ToArray());
		}
	}
}
