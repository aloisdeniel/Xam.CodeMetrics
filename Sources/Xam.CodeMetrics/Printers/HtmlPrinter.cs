
namespace Xam.CodeMetrics
{
	using System;
	using System.Linq;

	public class HtmlPrinter : IPrinter
	{
		public HtmlPrinter(Action<string> output)
		{
			this.ouput = output;
		}

		readonly Action<string> ouput;

		public void Print(Report report)
		{
			var chartValues = report.LinesOfCodeByCategory.Where(x => x.Key == "iOS" || x.Key == "Android" || x.Key == "Shared").Select(x => x.Value);

			this.ouput($@"

<html>
  <head>
	<script src=""https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.bundle.min.js"" type=""text/javascript""></script>
  </head>
  <body>
	<canvas id=""chart"" width=""400"" height=""400""></canvas>
	<script>s
		var ctx = document.getElementById(""chart"");
		var myPieChart = new Chart(ctx,{{ type: 'pie', data: {{ datasets: {{ data: [{string.Join(", ",chartValues)}] }} }} }});
	</script>
  </body>
</html>

".Trim());
		}
	}
}
