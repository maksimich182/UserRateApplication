using CurrencyGrpc;
using CurrencyService.BLL;
using Grpc.Core;

namespace CurrencyService.GrpcService;

public class GrpcCurrencyService : Currency.CurrencyBase
{
    private readonly IBllCurrencyService _bllCurrencyService;

    public GrpcCurrencyService(IBllCurrencyService bllCurrencyService)
    {
        _bllCurrencyService = bllCurrencyService;
    }

    public override async Task<GetUserCurrencyByIdResponse> GetUserCurrencyById(
        GetUserCurrencyByIdRequest request,
        ServerCallContext context)
    {
        var currencies = await _bllCurrencyService.GetUserCurrenciesById(request.UserId, context.CancellationToken);

        return new GetUserCurrencyByIdResponse
        {
            Currencies =
            {
                currencies.Select(ToCurrency)
            }
        };
    }

    private CurrencyModel ToCurrency(DataAccess.Repositories.Models.CurrencyModel currency)
        => new CurrencyModel
        {
            Name = currency.Name,
            Rate = currency.Rate
        };
}
