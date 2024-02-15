using NLog;
string path = Directory.GetCurrentDirectory() + "\\nlog.config";
// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();

// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();
string file = "data.txt";

if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    // int weeks;
    if (int.TryParse(Console.ReadLine(), out int weeks))
    {
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
    else
    {
        // log error
        logger.Error("You must enter a valid number");
    }
}
else if (resp == "2")
{
    // TODO: parse data file
    if (File.Exists(file))
    {



        StreamReader sr = new StreamReader(file);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();

            int total = 0;
            decimal avg = 0;


            // Found Here: https://stackoverflow.com/questions/1905850/how-do-i-convert-a-short-date-string-back-to-a-datetime-object
            // DateTime date = DateTime.ParseExact("12/15/2009", "MM/dd/yyyy", null);
            DateTime date = DateTime.ParseExact(line.Substring(0, line.IndexOf(",")), "M/d/yyyy", null);

            // Format Week Headers
            Console.WriteLine($"Week of {date:MMM}, {date:dd}, {date:yyyy}");

            // Format Day Headers
            Console.WriteLine($"{"Su",3} {"Mo",3} {"Tu",3} {"We",3} {"Th",3} {"Fr",3} {"Sa",3} {"Tot",3} {"Avg",3}");
            Console.WriteLine($"{"--",3} {"--",3} {"--",3} {"--",3} {"--",3} {"--",3} {"--",3} {"---",3} {"---",3}");

            string[] hourArr;
            int commaIndex = line.IndexOf(',');
            if (commaIndex != -1)
            {
                string substringAfterComma = line.Substring(commaIndex + 1);
                hourArr = substringAfterComma.Split('|');
            }
            else
            {
                // Handle case where ',' is not found in the line
                hourArr = line.Split('|');
            }

            // Because there are 2 extra columns in the table for tot and avg, run until count is over hourArr + 2
            // Make a temporary variable for count and string
            int count = 0;
            string str = "";

            while (count < hourArr.Length)
            {
                //https://stackoverflow.com/questions/1019793/how-can-i-convert-string-to-int
                total += int.Parse(hourArr[count]);
                count += 1;
            }

            avg = total / (decimal)count;

            Console.WriteLine($"{hourArr[0],3} {hourArr[1],3} {hourArr[2],3} {hourArr[3],3} {hourArr[4],3} {hourArr[5],3} {hourArr[6],3} {total,3} {Math.Round(avg, 1),3}");
            Console.WriteLine();
        }
        sr.Close();
    }
    else
    {
        Console.WriteLine("File does not exist");
    }
}
