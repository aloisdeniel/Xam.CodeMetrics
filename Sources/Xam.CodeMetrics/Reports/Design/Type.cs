namespace Xam.CodeMetrics
{
	using System;
	using System.Collections.Generic;

	public class Type
	{
		public enum MemberKind
		{
			Interface,
			Class,
			AbstractClass,
			StaticClass,
			Struct,
		}

		public Type(string name)
		{
			this.Name = name;
			this.Members = new List<Member>();
		}

		public string Name { get; }

		public List<Member> Members { get; }
	}
}
