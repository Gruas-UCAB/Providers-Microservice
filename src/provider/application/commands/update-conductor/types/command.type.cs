namespace ProvidersMicroservice.src.provider.application.commands.update_conductor.types
{
    public record UpdateConductorCommand
    (
        string ProviderId,
        string ConductorId,
        string? Location
    );
}
