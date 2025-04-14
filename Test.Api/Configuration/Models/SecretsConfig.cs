namespace Test.Api.Configuration.Models;

public class SecretsConfig
{
    public SecretValue<string>? One { get; set; }
    public SecretValue<int>? Two { get; set; }
    public SecretValue<bool>? Three { get; set; }
    public SecretValue<Guid>? Four { get; set; }
}
