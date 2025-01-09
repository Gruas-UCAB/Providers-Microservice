namespace ProvidersMicroservice.src.provider.application.commands.update_crane.types
{
    public record UpdateCraneCommand
    (
        string ProviderId,
        string CraneId
    );
}
