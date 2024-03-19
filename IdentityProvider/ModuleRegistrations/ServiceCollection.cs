﻿using IP.Services;

namespace IdentityProvider;

public static class ServiceCollection
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services)
    {
        return services
                .AddScoped<IOauthServices, OauthService>()
        ;
    }
}
