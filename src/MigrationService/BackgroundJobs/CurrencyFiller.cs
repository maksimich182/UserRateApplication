using MigrationService.DataAccess.Models;
using MigrationService.DataAccess.Repositories.CurrencyRepository;
using System.Text;
using System.Xml;

namespace MigrationService.BackgroundJobs;

public class CurrencyFiller : BackgroundService
{
    const string DATA_ADDRESS = "http://www.cbr.ru/scripts/XML_daily.asp";

    private readonly List<CurrencyModel> _storage;
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyFiller(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
        _storage = new List<CurrencyModel>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        XmlTextReader xmlReader = new XmlTextReader(DATA_ADDRESS);
        ParseXmlCurrency(xmlReader);

        foreach(var currency in _storage)
        {
            await _currencyRepository.CreateCurrencyAsync(currency);
        }
    }

    private void ParseXmlCurrency(XmlTextReader xmlReader)
    {
        var name = string.Empty;
        var value = string.Empty;

        while (xmlReader.Read())
        {
            if (xmlReader.NodeType == XmlNodeType.Element)
            {
                if (xmlReader.Name == "Name")
                {
                    name = xmlReader.ReadElementContentAsString();
                }
                if (xmlReader.Name == "Value")
                {
                    value = xmlReader.ReadElementContentAsString();
                }
            }

            if (name != string.Empty && value != string.Empty)
            {
                _storage.Add(
                    new CurrencyModel
                    {
                        Name = name,
                        Rate = double.Parse(value.Replace(',', '.'))
                    });

                name = string.Empty;
                value = string.Empty;
            }
        }
    }
}
