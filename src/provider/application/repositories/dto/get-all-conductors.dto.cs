namespace ProvidersMicroservice.src.provider.application.repositories.dto
{
    public record GetAllConductorsDto
    (
        int limit = 10,
        int offset = 1,
        bool active = true
    );
}
