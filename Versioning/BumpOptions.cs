using CommandLine;

[Verb("bump-package", aliases:new []{"b"})]
public class BumpOptions
{

    [Option('w',"what",HelpText = "What to bump")]
    public IEnumerable<string> Bump { get; set; }
    public async Task ApplyAsync()
    {
        Console.WriteLine($"You want to bump: {string.Join(',', this.Bump)}");

        if (this.Bump.Any(_ => _.Equals("package-patch", StringComparison.InvariantCultureIgnoreCase)))
        {
            Console.WriteLine("You want to bump the package patch.");
        }
        else
        {
            Console.WriteLine("You DO NOT want to bump the package patch.");
        }
        if (this.Bump.Any(_ => _.Equals("package-major", StringComparison.InvariantCultureIgnoreCase)))
        {
            Console.WriteLine("You want to bump the package major.");
        }
        else
        {
            Console.WriteLine("You DO NOT want to bump the package major.");
        }
    }
}