if (args.Length == 0 || args[0] == "-h" || args[0] == "--help")
{
    Console.WriteLine("Usage: gibberish <target>");
    Console.WriteLine("       options:");
    Console.WriteLine("         --output [value]         :   Writes output to this path");
    Console.WriteLine("         --render                 :   Writes output to console as well");
    Console.WriteLine("         --capitalization [value] :   none | upper | lower");
    Console.WriteLine("         --characterSet [value]   :   none | thai | georgian");
    Console.WriteLine("         --letter [value]         :   none | random | shuffle");
    Console.WriteLine("         --punctuation [value]    :   none | random | shuffle");
    return;
}

var consoleGibberish = new GibberishConsole.ConsoleGibberish(args);
consoleGibberish.Write();

