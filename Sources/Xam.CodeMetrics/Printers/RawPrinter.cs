namespace Xam.CodeMetrics
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class RawPrinter
	{
		public RawPrinter(Action<string> output)
		{
			this.ouput = output;
		}

		readonly Action<string> ouput;

		#region Helpers

		private void OutputLine(string text) => this.ouput($"{text}\n"); 

		private string Bar(char c) => new String(c,60);

		private void PrintH1(string title) => OutputLine($"\n\n{title.ToUpperInvariant()}\n{Bar(':')}");

		private void PrintH2(string title) => OutputLine($"\n\n{title}\n{Bar('=')}");

		private void PrintH3(string title) => OutputLine($"\n{title}\n{Bar('-')}");

		private void PrintListItem(string value, int level = 0) => OutputLine($"{(new String(' ',level * 2))}• {value}");

		private void PrintCollection<T1, T2>(IEnumerable<KeyValuePair<T1, T2>> collection)
		{
			foreach (var category in collection)
			{
				PrintListItem($"{category.Key}: {category.Value}");
			}
		}

		#endregion

		private void PrintLoc(Report report)
		{
			PrintH2("Lines of code");

			PrintH3("Total");
			OutputLine($"{report.TotalLinesOfCode}");

			PrintH3("By category");
			PrintCollection(report.LinesOfCodeByCategory.OrderBy(x => x.Key));

			PrintH3("By file extension");
			PrintCollection(report.LinesOfCodeByExtension.OrderBy(x => x.Key));

			PrintH3("Details");
			foreach (var category in report.LinesOfCode.OrderBy(x => x.Path))
			{
				OutputLine($"{category.Path.Replace(report.Path, string.Empty)}: {category.Count} ({string.Join(", ", category.Categories)})");
			}
		}

		private void PrintCommentLines(Report report)
		{
			PrintH2("Lines of comment");

			PrintH3("Total");
			OutputLine($"{report.TotalLinesOfComment}");

			PrintH3("By category");
			PrintCollection(report.LinesOfCommentByCategory.OrderBy(x => x.Key));

			PrintH3("By file extension");
			PrintCollection(report.LinesOfCommentByExtension.OrderBy(x => x.Key));

			PrintH3("Details");
			foreach (var category in report.LinesOfCode.OrderBy(x => x.Path))
			{
				OutputLine($"{category.Path.Replace(report.Path, string.Empty)}: {category.CommentsCount} ({string.Join(", ", category.Categories)})");
			}
		}

		private void PrintFiles(Report report)
		{
			PrintH2("Files");

			PrintH3("Total");
			OutputLine($"{report.TotalFilesOfCode}");

			PrintH3("By category");
			PrintCollection(report.FilesOfCodeByCategory.OrderBy(x => x.Key));
		}

		private void PrintDesign(Report report)
		{
			PrintH2("Design");

			PrintH3("Total");

			PrintH3("Detail");
			foreach (var dll in report.Design.OrderBy(x => x.Path))
			{
				PrintListItem($"{dll.Path.Replace(report.Path, string.Empty)}");
				dll.Projects.ToList().ForEach(project =>
				{
					PrintListItem(project.Name, 1);
					project.Types.ToList().ForEach(x => PrintListItem(x.Name, 2));
				});
			}
		}

		public void Print(Report report)
		{
			PrintH1(report.Name);
			PrintFiles(report);
			PrintLoc(report);
			PrintCommentLines(report);
			//PrintDesign(report);
		}
	}
}
