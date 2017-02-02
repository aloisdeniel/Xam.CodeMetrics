using System;
namespace Xam.CodeMetrics
{
	public class Member
	{
		public enum MemberKind
		{
			Property,
			Method,
			Event,
		}

		public Member(string name)
		{
			this.Name = name;
		}

		public MemberKind Kind { get; set; }

		public string Name { get; }

		public string[] Params { get; set; }
	}
}
