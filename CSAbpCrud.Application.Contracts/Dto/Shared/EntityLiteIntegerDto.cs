using Volo.Abp.Application.Dtos;

namespace CSAbpCrud.Application.Contracts.Dto.Shared
{
    public class EntityLiteIntegerDto : EntityDto<int>
    {
        public string Label { get; set; }
        public string Code { get; set; }
    }
}