namespace IronMQSender
{
    using System;
    using IronMQ;

    public class IronMQSender
    {
        public static void Main()
        {
            Client client = new Client("56430b319195a8000700000d", "IsjQFsbII7uW3eKBq6zU");
            Queue queue = client.Queue("LiveChat");

            while (true)
            {
                Console.WriteLine("Please enter message to be sent:");
                string msg = Console.ReadLine();
                queue.Push(msg);
                Console.WriteLine("Message sent to IronMQ server.");
            }
        }
    }
}
