using Gibberish;
using Gibberish.Enums;

namespace GibberishConsole;

public class ConsoleGibberish
{
    private string TargetFilePath { get; set; } = "";
    private string TargetFileContent { get; set; } = "";

    private string OutputFilePath { get; set; } = "";
    
    private bool OutputToConsole { get; set; }

    private GibberishFormatter Formatter { get; } = new();

    private List<string> ArgList { get; }

    public ConsoleGibberish(IEnumerable<string> args)
    {
        ArgList = args.ToList();

        GetTargetFile();
        BuildOutputFile();
        BuildFormatterConfig();
    }

    public void Write()
    {
        var output = TargetFileContent.ToGibberish(Formatter);
        if (OutputToConsole) Console.Write(output);
        
        File.WriteAllText(OutputFilePath, output);
    }

    private void BuildFormatterConfig()
    {
        // --capitalization <<none|upper|lower>>
        var capitalization = GetArgValue("capitalization");
        if (!string.IsNullOrEmpty(capitalization))
        {
            Formatter.CapitalizationBehavior = ParseEnum<CapitalizationBehavior>(capitalization);
        }
        
        // --characterSet <<none|thai|georgian>> 
        var characterSet = GetArgValue("characterSet");
        if (!string.IsNullOrEmpty(characterSet))
        {
            Formatter.CharacterSetReplacementBehavior = ParseEnum<CharacterSetReplacementBehavior>(characterSet);
        }
        
        // --letter <<none|random|shuffle>>
        var letter = GetArgValue("letter");
        if (!string.IsNullOrEmpty(letter))
        {
            Formatter.LetterOrderBehavior = ParseEnum<LetterOrderBehavior>(letter);
        }
        
        // --punctuation <<none|random|shuffle>>
        var punctuation = GetArgValue("punctuation");
        if (!string.IsNullOrEmpty(punctuation))
        {
            Formatter.PunctuationOrderBehavior = ParseEnum<PunctuationOrderBehavior>(punctuation);
        }
    }
    private string? GetArgValue(string key)
    {
        var index = GetArgIndex(key);
        if (index + 1 < ArgList.Count && index > 0)
        {
            return ArgList[index];
        }

        return null;
    }
    private int GetArgIndex(string key)
    {
        return ArgList.FindIndex(m => m.ToLower().Equals($"--{key}"));
    }
    private void GetTargetFile()
    {
        var path = ArgList.First();

        if (!File.Exists(path))
        {
            throw new FileNotFoundException(path);
        }

        TargetFilePath = Path.GetFullPath(path);
        TargetFileContent = File.ReadAllText(path);
    }
    private void BuildOutputFile()
    {
        OutputToConsole = GetArgIndex("render") > 0;
        
        var specifiedPath = GetArgValue("--output");
        if (!string.IsNullOrEmpty(specifiedPath))
        {
            OutputFilePath = Path.GetFullPath(OutputFilePath);
            return;
        }

        const string defaultNameAppend = " - gibberish";
        var directoryName = Path.GetDirectoryName(TargetFilePath);
        var fileName = Path.GetFileNameWithoutExtension(TargetFilePath) + defaultNameAppend +
                       Path.GetExtension(TargetFilePath);

        if (directoryName == null) return;
        OutputFilePath = Path.Combine(directoryName, fileName);

        var append = 1;
        while (File.Exists(OutputFilePath))
        {
            fileName = Path.GetFileNameWithoutExtension(TargetFilePath) + $"{defaultNameAppend} {append++}" +
                       Path.GetExtension(TargetFilePath);
            OutputFilePath = Path.Combine(directoryName, fileName);
        }
    }
    private static T ParseEnum<T>(string value)
    {
        return (T) Enum.Parse(typeof(T), value, true);
    }
}