using FluentMigrator.Runner;

namespace MigrationService;

public static class HostExtension
{
    public static async Task RunOrMigrate(
        this IHost host,
        string[] args)
    {
        if (!IsNeedMigration(args))
        {
            await host.RunAsync();
        }

        var scope = host.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.StartMigrate(args);
        await Task.CompletedTask;
    }

    private static bool IsNeedMigration(string[] args)
    {
        return args.Length > 0 && args[0] == "migrate";
    }

    private static void StartMigrate(this IMigrationRunner runner, string[] args)
    {
        switch (args[1])
        {
            case "down":
                if (args.Length == 3)
                    runner.MigrateDown(int.Parse(args[2]));
                runner.MigrateDown(0);
                break;
            case "up":
                if (args.Length == 3)
                    runner.MigrateUp(int.Parse(args[2]));
                runner.MigrateUp();
                break;
            default: throw new ArgumentException("Wrong parameters of migration");
        }
    }
}
