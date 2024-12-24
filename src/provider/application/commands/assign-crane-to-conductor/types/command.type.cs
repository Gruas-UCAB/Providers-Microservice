namespace ProvidersMicroservice.src.provider.application.commands.assign_crane_to_conductor.types
{
    public record AssignCraneToConductorCommand(
    string ProviderId,
    string ConductorId,
    string CraneId
 );
}
