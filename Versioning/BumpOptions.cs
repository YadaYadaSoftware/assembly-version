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

        ProjectRootElement p = ProjectRootElement.Open(this.Source);
        foreach (var projectPropertyElement in p.Properties)
        {
            Console.WriteLine(projectPropertyElement.Name);
        }

        var version = p.Properties.SingleOrDefault(_ => _.Name == "Version");
        version.Value = "2.0.0";
        p.Save();
        //var solutionFile = Microsoft.Build.Construction.SolutionFile.Parse(@"C:\Users\17034\source\repos\YadaYadaSoftware\assembly-version\Versioning.sln");
        //foreach (var projectInSolution in solutionFile.ProjectsInOrder)
        //{
        //    Console.WriteLine(projectInSolution.ProjectName);
        //    ProjectRootElement p = ProjectRootElement.Open(projectInSolution.AbsolutePath);
        //    foreach (var projectPropertyElement in p.Properties)
        //    {
        //        Console.WriteLine(projectPropertyElement.Name);
        //    }

        //}



    }
}