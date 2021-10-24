using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATLib;
using System.Net;
using System.ComponentModel;

namespace ATDownloader
{
    class Program
    {
        static string filename;
        static string url;

        static void Main(string[] args)
        {
            Console.Title = "ATDownloader v1.0 - https://github.com/miso-xyz/ATDownloader";
            if (args.Count() == 0) { Console.Write("Enter link: "); url = Console.ReadLine(); }
            else { url = args[0]; }
            Console.WriteLine("Getting Track Information...");
            Console.ForegroundColor = ConsoleColor.Cyan;

            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);

            // Actual Code
            ATLib.Music music = new ATLib.Music(url);
            filename = music.Artist.Name + " - " + music.Title + ".ogg";
            wc.DownloadFileAsync(new Uri(music.apiURL + "?platform=1&ref=website&X-Cular-Session=?t"), filename); // removing "&X-Cular-Session=" causes a 400 error
            Console.ReadKey();
        }

        static void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Finished!");
            Console.ResetColor();
            Console.WriteLine(" (Press any key to exit...)");
            Console.ReadKey();
        }

        static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Now Downloading '" + filename + "'... (" + e.BytesReceived + "/" + e.TotalBytesToReceive + " - " + e.ProgressPercentage + "%)");
        }
    }
}
