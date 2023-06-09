﻿using Microsoft.Extensions.DependencyInjection;

namespace KavirTire.Shop.Infrastructure.Persistence.Common;

public static class DbInitializerExtensions
{
    public static async Task InitializeDb(this IServiceScope serviceScope)
    {
        var initializer = serviceScope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initializer.InitialiseAsync();
        await initializer.SeedAsync();
    }
}