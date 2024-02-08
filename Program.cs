// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // TODO: create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = int.Parse(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new Random();
    // create file
    StreamWriter sw = new StreamWriter("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
    // TODO: parse data file
    if (File.Exists(file))
    {

        int total = 0;
        int avg = 0;

        StreamReader sr = new StreamReader(file);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();

            // Found Here: https://stackoverflow.com/questions/1905850/how-do-i-convert-a-short-date-string-back-to-a-datetime-object
            // DateTime date = DateTime.ParseExact("12/15/2009", "MM/dd/yyyy", null);
            DateTime date = DateTime.ParseExact(line.Substring(0, line.IndexOf(",")), "M/dd/yyyy", null);

            // Format Week Headers
            Console.WriteLine($"Week of {date:MMM}, {date:dd}, {date:yyyy}");

            // Format Day Headers
            Console.WriteLine($"{"Su",3} {"Mo",3} {"Tu",3} {"We",3} {"Th",3} {"Fr",3}  {"Sa",3} {"Tot",3} {"Avg",3}");
            Console.WriteLine($"{"--",3} {"--",3} {"--",3} {"--",3} {"--",3} {"--",3}  {"--",3} {"---",3} {"---",3}");


        }
        sr.Close();
    }
    else
    {
        Console.WriteLine("File does not exist");
    }
}
