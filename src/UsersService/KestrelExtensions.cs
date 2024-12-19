using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace UsersService;

internal static class KestrelExtensions
{
    public static void ListenPortByOptions(
            this KestrelServerOptions option,
            string envOption,
            HttpProtocols httpProtocols)
    {
        var isHttpPortParsed = int.TryParse(Environment.GetEnvironmentVariable(envOption), out var httpPort);

        if (isHttpPortParsed)
        {
            option.Listen(IPAddress.Any, httpPort, options => options.Protocols = httpProtocols);
        }
    }
}
