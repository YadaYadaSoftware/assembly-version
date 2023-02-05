using System.Diagnostics;
using CommandLine;
using Microsoft.Build.Construction;
using Microsoft.Build.Definition;
using Microsoft.Build.Evaluation.Context;

[Verb("bump-package", aliases:new []{"b"})]
public class BumpOptions
{

    const string packagePatchName = "package-patch";
    const string assemblyMajorName = "assembly-major";

    [Option('w',"what",HelpText = $"What to bump:[{packagePatchName},{assemblyMajorName}]")]
    public IEnumerable<string> Bump { get; set; }

    [Option('s',"source",HelpText = "Source to bump")]
    public string Source { get; set; }
    public async Task ApplyAsync()
    {
        // Console.WriteLine($"You want to bump: {string.Join(',', this.Bump)}");

        var notHandled = new List<string>(this.Bump);

        var packagePatch = notHandled.Contains(packagePatchName);
        if (packagePatch) notHandled.Remove(packagePatchName);

        var assemblyMajor = notHandled.Contains(assemblyMajorName);
        if (assemblyMajor) notHandled.Remove(assemblyMajorName);


        ProjectRootElement p = ProjectRootElement.Open(this.Source);

        var versionProperty = p.Properties.SingleOrDefault(_ => _.Name == "Version");

        

        Version v;
        try
        {
            v = new Version(versionProperty.Value);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Cannot parse version of {versionProperty.Value}: {e}", e);
        }

        if (packagePatch)
        {
            var patch = v.Build;
            var newPatch = v.Build + 1;
            // Console.WriteLine($"Incrementing {nameof(v.Build)} from '{patch}' to '{newPatch}");
            v = new Version(v.Major, v.Minor, newPatch);
        }
        else
        {
            // Console.WriteLine("You DO NOT want to bump the package patch.");
        }
        if (assemblyMajor)
        {
            // Console.WriteLine("You want to bump the assembly major.");
        }
        else
        {
            // Console.WriteLine("You DO NOT want to bump the assembly major.");
        }

        if (notHandled.Any())
        {
            throw new InvalidOperationException($"The following are not valid bumps: {string.Join(',',notHandled)}.");
        }


        versionProperty.Value = v.ToString();


        // Console.WriteLine($"New Version:{v}");

        p.Save();

        Console.WriteLine(v.ToString());

    }
}