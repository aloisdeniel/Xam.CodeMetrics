namespace Xam.CodeMetrics
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Microsoft.CodeAnalysis;
	using Microsoft.CodeAnalysis.CSharp;
	using Microsoft.CodeAnalysis.CSharp.Syntax;
	using Microsoft.CodeAnalysis.MSBuild;

	public class Solution
	{
		public Solution(string sln)
		{
			this.Path = sln;

			// start Roslyn workspace
			var workspace = MSBuildWorkspace.Create();

			var solution = workspace.OpenSolutionAsync(sln).Result;

			foreach (var project in solution.Projects)
			{
				var projectDesign = new Project();
				Compilation compilation;
				if (project.TryGetCompilation(out compilation))
				{
					foreach (var typeName in compilation.Assembly.TypeNames)
					{
						var typeDesign = new Type(typeName);


					}

				}

				this.Projects.Add(projectDesign);
			}
		}

		private Assembly assembly;

		public string Path { get; }

		public List<Project> Projects { get; } = new List<Project>();
	}
}
