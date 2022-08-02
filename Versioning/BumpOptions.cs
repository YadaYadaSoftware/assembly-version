using CommandLine;

[Verb("bump-package", aliases:new []{"b"})]
public class BumpOptions
{

    [Option('w',"what",HelpText = "What to bump")]
    public IEnumerable<string> Bump { get; set; }
    public async Task ApplyAsync()
    {
        Console.WriteLine($"You want to bump: {string.Join(',', this.Bump)}");
    }
}