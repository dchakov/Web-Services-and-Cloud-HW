namespace Dropbox
{
    using Api;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StartUp
    {
        private const string DropboxAppKey = "yf6ze9we2e27u1w";
        private const string DropboxAppSecret = "y2af8xgm9fke5lz";

        private const string OAuthTokenFileName = "OAuthTokenFileName.txt";

        public static void Main()
        {
            if (!File.Exists("C:\\Users\\Dell5558\\Desktop"))
            {

            }
            DropboxClient client = new DropboxClient();
        }
    }
}
