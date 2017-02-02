namespace Xam.CodeMetrics
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class Report
	{
		public Report(string folder, IEnumerable<Loc> loc = null, IEnumerable<Solution> design = null)
		{
			this.Date = DateTime.Now;
			this.Path = folder;
			this.Name = new DirectoryInfo(folder).Name;
			this.LinesOfCode = loc ?? new Loc[0];
			this.Design = design ?? new Solution[0];
		}

		public DateTime Date { get; }

		public string Name { get; }

		public string Path { get;  }

		public IEnumerable<Solution> Design { get; }

		public IEnumerable<Loc> LinesOfCode { get; }

		public Report Merge(Report other) => new Report(this.Path,this.LinesOfCode.Union(other.LinesOfCode), this.Design.Union(other.Design));

		#region Lines

		public int TotalLinesOfCode => this.LinesOfCode.Sum(x => x.Count);

		public int TotalLinesOfComment => this.LinesOfCode.Sum(x => x.CommentsCount);

		public Dictionary<string, int> LinesOfCodeByExtension => this.LinesOfCode
		                                                             .GroupBy(x => System.IO.Path.GetExtension(x.Path).TrimStart('.'))
																	 .ToDictionary(x => x.Key, x => x.Sum(c => c.Count));

		// TODO factorize
		public Dictionary<string, int> LinesOfCommentByExtension => this.LinesOfCode
																	 .GroupBy(x => System.IO.Path.GetExtension(x.Path).TrimStart('.'))
		                                                                .ToDictionary(x => x.Key, x => x.Sum(c => c.CommentsCount));

		public Dictionary<string, int> LinesOfCodeByCategory => this.LinesOfCode.SelectMany(x => x.Categories)
															       			    .Distinct()
															                    .ToDictionary(
			                                                            			x=> x, 
		                                                                            x=> this.LinesOfCode
			                                                            						.Where((l) => l.Categories.Contains(x))
			                                                            						.Select(l => l.Count)
			                                                            						.Sum());
		// TODO factorize
		public Dictionary<string, int> LinesOfCommentByCategory => this.LinesOfCode.SelectMany(x => x.Categories)
																				   .Distinct()
																				.ToDictionary(
																					x => x,
																					x => this.LinesOfCode
																								.Where((l) => l.Categories.Contains(x))
			                                                              						 .Select(l => l.CommentsCount)
																								.Sum());

		#endregion

		#region Files

		public int TotalFilesOfCode => this.LinesOfCode.Count();


		public Dictionary<string, int> FilesOfCodeByCategory => this.LinesOfCode.SelectMany(x => x.Categories)
																				   .Distinct()
																					.ToDictionary(
																						x => x,
			                                                            				x => this.LinesOfCode.Where((l) => l.Categories.Contains(x)).Count());

		#endregion
	}
}
