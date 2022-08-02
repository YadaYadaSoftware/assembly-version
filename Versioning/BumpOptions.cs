using CommandLine;

[Verb("bump", aliases:new []{"b"})]
public class BumpOptions
{
    public async Task ApplyAsync()
    {
        Console.WriteLine("You Want To Bump");
    }
}