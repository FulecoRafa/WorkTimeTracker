using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using WorkTimeTracker.Actions;

namespace WorkTimeTracker
{
    class Program
    {
        static FileInfo defaultConfigFile = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.wttrc");

        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("Simple app to keep track off worked and to work hours");

            var showCommand = new Command("show", "Show entries for work day");

            var dateOption = new Option<DateOnly>(
                name: "--date",
                description: "Date to show entries for",
                getDefaultValue: () => DateOnly.FromDateTime(DateTime.Now)
            );
            dateOption.AddAlias("-d");
            showCommand.AddOption(dateOption);

            rootCommand.AddCommand(showCommand);
            showCommand.SetHandler((date) => Runner.Show(8, date), dateOption);

            var addCommand = new Command("add", "Add entry for work day");
            addCommand.SetHandler(() => Runner.Add());
            rootCommand.AddCommand(addCommand);

            var commandLineBuilder = new CommandLineBuilder(rootCommand);
            //commandLineBuilder.AddMiddleware(
            //    async (context, next) =>
            //    {
            //    }
            //);
            commandLineBuilder.UseDefaults();

            var parser = commandLineBuilder.Build();

            return await parser.InvokeAsync(args);
        }
    }
}