namespace Dropbox
{
    using DropNet;
    using System;
    using System.Diagnostics;
    using System.IO;

    public class StartUp
    {
        // http://dkdevelopment.net/what-im-doing/dropnet/

        private const string DropboxAppKey = "yf6ze9we2e27u1w";
        private const string DropboxAppSecret = "y2af8xgm9fke5lz";

        public static void Main()
        {
            var client = new DropNetClient(DropboxAppKey, DropboxAppSecret);

            var token = client.GetToken();
            var url = client.BuildAuthorizeUrl();

            Console.WriteLine("Open browser with in : {0}", url);
            Console.WriteLine("Press enter when clicked allow");
            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
              url);

            Console.ReadLine();
            var accessToken = client.GetAccessToken();

            client.UseSandbox = true;
            var metaData = client.CreateFolder("Pictures" + DateTime.Now.ToString());

            string[] dir = Directory.GetFiles("../../", "*.jpg");
            foreach (var item in dir)
            {
                Console.WriteLine("Uploading.....");
                FileStream stream = File.Open(item, FileMode.Open);
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);

                client.UploadFile(metaData.ToString(), item.Substring(6), bytes);

                stream.Close();
            }
            var picUrl = client.GetShare(metaData.Path);
            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
             picUrl.Url);
        }
    }
}
