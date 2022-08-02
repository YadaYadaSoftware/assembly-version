using CommandLine;

[Verb("bump-package", aliases:new []{"b"})]
public class BumpOptions
{

    [Option('p',"patch",HelpText = "Bump The Patch Version")]
    public bool Patch { get; set; }
    public async Task ApplyAsync()
    {
        if (this.Patch)
        {
            Console.WriteLine("You want to bump the patch");
        }
        else
        {
            Console.WriteLine("You DO NOT want to bump the patch");
        }
    }
}