using HrManagement.Infrastructure;
using Microsoft.Extensions.Options;

namespace HrManagement.Api.OptionsSetup;

public class JwtOptionSetup : IConfigureOptions<JwtOption>
{
    private const string Jwt = nameof(Jwt);
    private readonly IConfiguration _configuration;

    public JwtOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOption options)
    {
        _configuration.GetSection(Jwt).Bind(options);
    }
}