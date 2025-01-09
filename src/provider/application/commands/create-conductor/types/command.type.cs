namespace ProvidersMicroservice.src.provider.application.commands.create_conductor.types
{
    public record CreateConductorCommand(
        string ProviderId,
        string ConductorId,
        int Dni,
        string Name,
        string Location,
        string Image,
        string CraneId
     );
}
