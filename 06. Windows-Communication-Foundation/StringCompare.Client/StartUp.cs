namespace StringCompare.Client
{
    using System;
    using StringCompare.Client.ServiceStringCompare;

    public class StartUp
    {
        public static void Main()
        {
            var client = new StringCompareClient();

            var result = client.Contains("we", "weweweweoojjj ssdaa");

            Console.WriteLine(result);
        }
    }
}
