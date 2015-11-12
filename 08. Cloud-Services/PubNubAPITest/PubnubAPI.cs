using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace PubNubMessaging.Core
{
    public class PubnubAPI
    {
        public static void Main()
        {
            Process.Start("..\\..\\Receiver.html");

            Thread.Sleep(2000);

            Pubnub pubnub = new Pubnub(
                publishKey: "pub-c-ebd958d0-e2fd-4aab-a2fc-7d31e8995644",
                subscribeKey: "sub-c-44a39b0c-886a-11e5-84ee-0619f8945a4f"
            );
            string channel = "Chat";

            pubnub.Subscribe(
                channel,
                delegate (object message)
                {
                    Console.WriteLine("Received Message -> '" + message + "'");
                },
                delegate (object message)
                {
                    Console.WriteLine("Received Message -> '" + message + "'");
                },
                delegate (PubnubClientError message)
                {
                    Console.WriteLine("Received Message -> '" + message + "'");
                }
            );

            while (true)
            {
                Console.Write("Enter a message to be sent to Pubnub: ");
                string msg = Console.ReadLine();
                pubnub.EnableJsonEncodingForPublish = false;
                pubnub.Publish(channel, msg,
                    delegate (object message)
                {
                    Console.WriteLine("Received Message -> '" + message + "'");
                },
                    delegate (PubnubClientError message)
                    {
                        Console.WriteLine("Received Message -> '" + message + "'");
                    });
                Console.WriteLine("Message {0} sent.", msg);
            }
        }
    }
}
