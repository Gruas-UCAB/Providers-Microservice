namespace ProvidersMicroservice.src.provider.application.commands.create_conductor.types
{
    public record CreateConductorCommand(
        string ProviderId,
        int Dni,
        string Name,
        string Image,
        string? CraneId
     );
}
