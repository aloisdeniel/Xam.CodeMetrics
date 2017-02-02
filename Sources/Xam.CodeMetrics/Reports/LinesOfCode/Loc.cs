namespace Xam.CodeMetrics
{
	using System;

	public class Loc
	{
		public Loc(string path, int count, int commentLines, string[] categories)
		{
			this.Path = path;
			this.Count = count;
			this.Categories = categories;
			this.CommentsCount = commentLines;
		}

		public string Path { get; }

		public int Count { get; }

		public int CommentsCount { get; }

		public string[] Categories { get; }
	}
}
