namespace IronMQReceiver
{
    using System;
    using IronMQ.Data;
    using IronMQ;
    using System.Threading;

    public class IronMQReceiver
    {
        public static void Main()
        {
            Client client = new Client("56430b319195a8000700000d", "IsjQFsbII7uW3eKBq6zU");
            Queue queue = client.Queue("LiveChat");

            Console.WriteLine("Listening for new messages from IronMQ server:");
            while (true)
            {
                Message msg = queue.Get();
                if (msg != null)
                {
                    Console.WriteLine(string.Format("{{{0}:{1}}}", msg.Id, msg.Body));
                    queue.DeleteMessage(msg);
                }
                Thread.Sleep(100);
            }
        }
    }
}
