namespace ProvidersMicroservice.src.provider.application.repositories.dto
{
    public record UpdateConductorDto
    (
        string ConductorId,
        string? Location
    );
}
