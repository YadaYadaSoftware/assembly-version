using CommandLine;

Console.WriteLine("Hello");

Console.WriteLine($"{nameof(args)}='{string.Join(';', args)}'");


await Parser.Default.ParseArguments<BumpOptions>(args)
        .WithParsedAsync<BumpOptions>(async o =>
        {
            await o.ApplyAsync();
        });