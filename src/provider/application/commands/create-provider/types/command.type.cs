namespace ProvidersMicroservice.src.providers.application.commands.create_provider.types
{
    public record CreateProviderCommand(
        string Id,
        string Rif,
        string Name,
        string Image
     );
}
