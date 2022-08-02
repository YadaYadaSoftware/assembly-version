using CommandLine;

[Verb("bump-package", aliases:new []{"b"})]
public class BumpOptions
{

    const string packagePatchName = "package-patch";
    const string assemblyMajorName = "assembly-major";

    [Option('w',"what",HelpText = $"What to bump:[{packagePatchName},{assemblyMajorName}]")]
    public IEnumerable<string> Bump { get; set; }
    public async Task ApplyAsync()
    {
        Console.WriteLine($"You want to bump: {string.Join(',', this.Bump)}");

        var notHandled = new List<string>(this.Bump);

        var packagePatch = notHandled.Contains(packagePatchName);
        if (packagePatch) notHandled.Remove(packagePatchName);

        var assemblyMajor = notHandled.Contains(assemblyMajorName);
        if (assemblyMajor) notHandled.Remove(assemblyMajorName);



        if (packagePatch)
        {
            Console.WriteLine("You want to bump the package patch.");
        }
        else
        {
            Console.WriteLine("You DO NOT want to bump the package patch.");
        }
        if (assemblyMajor)
        {
            Console.WriteLine("You want to bump the assembly major.");
        }
        else
        {
            Console.WriteLine("You DO NOT want to bump the assembly major.");
        }

        if (notHandled.Any())
        {
            throw new InvalidOperationException($"The following are not valid bumps: {string.Join(',',notHandled)}.");
        }
    }
}