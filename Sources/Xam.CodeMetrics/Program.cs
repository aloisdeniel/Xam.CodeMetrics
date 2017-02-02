namespace Xam.CodeMetrics
{
	using System;
	using System.IO;
	using System.Linq;

	class MainClass
	{
		public static void Main(string[] args)
		{
			var folder = args.LastOrDefault() ?? Directory.GetCurrentDirectory();

			var analyzer = new Analyzer();

			analyzer.Register<SharedCSharpLocCounter>();
			analyzer.Register<UnitTestCSharpLocCounter>();
			//analyzer.Register<FormsXamlLocCounter>();
			//analyzer.Register<AndroidXmlLocCounter>();
			analyzer.Register<AndroidCSharpLocCounter>();
			analyzer.Register<IOSSharpLocCounter>();
			analyzer.Register<UITestCSharpLocCounter>();

			var report = analyzer.Analyze(folder);

			var printer = new RawPrinter(x => Console.Write(x));
			printer.Print(report);


			new HtmlPrinter(x =>
			{
				var ts = report.Date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
				var p = Path.Combine(folder, $"Metrics-{ts}.html");
				File.Create(p).Dispose();
				File.WriteAllText(p, x);
			}).Print(report);
		}

	}
}
