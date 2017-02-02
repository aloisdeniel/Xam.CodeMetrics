using System;
using System.Collections.Generic;

namespace Xam.CodeMetrics
{
	public class Project
	{
		public Project()
		{
			this.Types = new List<Type>();
		}

		public List<Type> Types { get; }

		public string Name { get; set; }
	}
}
