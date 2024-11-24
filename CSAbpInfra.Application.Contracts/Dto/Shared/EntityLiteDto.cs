using Volo.Abp.Application.Dtos;

namespace CSAbpInfra.Application.Contracts.Dto.Shared;

public class EntityLiteDto : EntityDto<Guid>
{
    public string Label { get; set; }
}