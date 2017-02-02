namespace Xam.CodeMetrics
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class Analyzer
	{
		public Analyzer()
		{
		}

		private List<LocCounter> counters = new List<LocCounter>();

		public void Register<T>() where T : LocCounter
		{
			this.counters.Insert(0, Activator.CreateInstance<T>());
		}

		public Loc GetLinesOfCode(string path)
		{
			var counter = counters.FirstOrDefault(x => x.IsValid(path));
			
			var loc = counter?.Count(path);
			return loc;
		}

		public IEnumerable<Loc> GetAllLinesOfCode(string folder)
		{
			var files = Directory.GetFiles(folder)
								 .Select(x => x.Replace('\\', '/'))
								 .Where(x => !IgnoredContainedPath.Any(c => x.Contains($"/{c}/")));

			return files.Select(x => GetLinesOfCode(x)).Where(x => x != null).ToList();
		}


		public IEnumerable<Solution> GetAllDesign(string folder)
		{
			var files = Directory.GetFiles(folder)
								 .Select(x => x.Replace('\\', '/'))
			                     .Where(x => !IgnoredContainedPath.Any(c => x.Contains($"/{c}/")) && Path.GetExtension(x) == ".sln");

			return files.Select(x => new Solution(x)).ToList();
		}

		public static readonly string[] IgnoredContainedPath = { "bin", "obj", "packages" };

		public Report Analyze(string folder)
		{
			var locs = GetAllLinesOfCode(folder);
			var design = new Solution[0];// GetAllDesign(folder);

			var report = new Report(folder, locs, design);
			
			foreach (string subdir in Directory.GetDirectories(folder))
			{
				var subreport = Analyze(subdir);
				report = report.Merge(subreport);
			}

			return report;
		}
	}
}
