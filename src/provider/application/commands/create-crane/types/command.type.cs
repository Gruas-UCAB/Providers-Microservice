namespace ProvidersMicroservice.src.crane.application.commands.create_crane.types
{
    public record CreateCraneCommand(
        string ProviderId,
        string Brand,
        string Model,
        string Plate,
        string Type,
        int Year
     );
}
