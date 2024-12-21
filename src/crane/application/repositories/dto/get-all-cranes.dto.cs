namespace ProvidersMicroservice.src.crane.application.repositories.dto
{
    public record GetAllCranesDto
    (
        int limit = 10,
        int offset = 1,
        bool active = true
    );
}
