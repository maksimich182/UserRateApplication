﻿namespace MigrationService.DataAccess.Models;

public record CurrencyModel
{
    public required string Name { get; init; }
    public required double Rate { get; init; }
}