using Xamarin.Forms;

namespace Xam.CodeMetrics.Sample
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			/*
			 * 
			 * Multi line
			 *  //
		
			 * */
			MainPage = new Xam_CodeMetrics_SamplePage();
		}
		//// <summary>
		/// Ons the start.
		/// </summary>
		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
