namespace GetWeekDay.Client
{
    using System;
    using GetWeekDay.Client.ServiceReferenceWeekDay;

    public class StartUp
    {
        public static void Main()
        {
            ServiceWeekDayClient weekDayService = new ServiceWeekDayClient(); 

            Console.WriteLine(weekDayService.GetData(DateTime.Now));
        }
    }
}
