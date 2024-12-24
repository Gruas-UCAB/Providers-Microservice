namespace ProvidersMicroservice.src.provider.application.commands.unassign_crane_to_conductor.types
{
    public record UnassignCraneToConductorCommand(
    string ProviderId,
    string ConductorId,
    string CraneId
 );
}
