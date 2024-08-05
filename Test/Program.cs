

var begin = new DateTime(2020, 1, 1);
DateTime? end = null;

if (!(end != null && end <= begin))
{
    Console.WriteLine("End date must be greater than begin date");
}


Console.ReadLine();