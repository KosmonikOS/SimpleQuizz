using SimpleQuizz_Prototype.Services.Implementations;
using SimpleQuizz_Prototype.Services.Interfaces;

namespace SimpleQuizz_Prototype.Extentions;
public static class ServicesExtention
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddSingleton<IHostQuizzMapper, HostQuizzMapper>();
        return services;
    }
}
