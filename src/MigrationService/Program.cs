using Microsoft.AspNetCore.Server.Kestrel.Core;
using MigrationService;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(
    builder => builder.UseStartup<Startup>())
    .Build()
    .RunOrMigrate(args);
