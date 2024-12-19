using Grpc.Core;
using UsersGrpc;
using UsersService.BLL;
using UsersService.DataAccess.Repositories.Models;

namespace UsersService.GrpcService;

public class GrpcUsersService : Users.UsersBase
{
    private readonly IBllUsersService _bllUsersService;

    public GrpcUsersService(IBllUsersService bllUsersService)
    {
        _bllUsersService = bllUsersService;
    }

    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        await _bllUsersService.CreateUser(
            new UserModel 
            { 
                Name = request.Name, 
                CurrenciesIds = request.CurrenciesIds.ToArray() 
            }, 
            context.CancellationToken);

        return new CreateUserResponse { };
    }
}
