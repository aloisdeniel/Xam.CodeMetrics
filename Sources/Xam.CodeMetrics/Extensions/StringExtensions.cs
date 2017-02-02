using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Xam.CodeMetrics
{
	public static class StringExtensions
	{
		public static bool IsProjectType(this string path, string type)
		{
			return Regex.Match(path, $@"(/{type}/)|(/{type}.)|(.{type}/)|(.{type}.)").Success;
		}

		public static int CountLines(this string text) => text.Count(c => c == '\n') + 1;
	}
}
