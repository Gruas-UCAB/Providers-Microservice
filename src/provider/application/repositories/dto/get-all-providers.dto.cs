namespace ProvidersMicroservice.src.provider.application.repositories.dto
{
    public record GetAllProvidersDto
    (
        int limit = 10,
        int offset = 1,
        bool active = true
    );
}
