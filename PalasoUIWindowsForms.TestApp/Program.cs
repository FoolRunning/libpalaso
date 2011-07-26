using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Palaso.UI.WindowsForms.ImageGallery;
using Palaso.UI.WindowsForms.WritingSystems;
using Palaso.WritingSystems.Migration.WritingSystemsLdmlV0To1Migration;

namespace PalasoUIWindowsForms.TestApp
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

#if  TESTING_ISOLookup
			var dialog = new LookupISOCodeDialog();
			dialog.ISOCode = "etr";
			Application.Run(dialog);
#endif

//#if  TESTING_WS
			string tempPath = Path.GetTempPath() + "WS-Test";
			Directory.CreateDirectory(tempPath);
			try
			{
				Application.Run(new WritingSystemSetupDialog(tempPath, onMigration));
			}
			catch (Exception)
			{
				throw;
			}
//#endif
#if TESTING_ARTOFREADING
			var images = new ArtOfReadingImageCollection();
			images.LoadIndex(@"C:\palaso\output\debug\ImageGallery\artofreadingindexv3_en.txt");
			images.RootImagePath = @"c:\art of reading\images";
			var form = new PictureChooser(images, "duck");
			Application.Run(form);
			Console.WriteLine("REsult: " + form.ChosenPath);
#endif
		}

		public static void onMigration(IEnumerable<LdmlVersion0MigrationStrategy.MigrationInfo> migrationInfo)
		{
		}
	}
}