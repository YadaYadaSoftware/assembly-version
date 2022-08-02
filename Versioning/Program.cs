using CommandLine;

Console.WriteLine("Hello");

await Parser.Default.ParseArguments<BumpOptions>(args)
        .WithParsedAsync<BumpOptions>(async o =>
        {
            await o.ApplyAsync();
        });