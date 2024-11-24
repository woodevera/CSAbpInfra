using CSAbpCrud.Application.Contracts.Enums;

namespace CSAbpCrud.Application.Contracts.Dto.Shared
{
    public class FieldSignatureDto
    {
        public FieldSignatureDto(string name, SignatureDataTypeEnum dataType)
        {
            Name = name;
            DataType = dataType;
        }
        public string Name { get; }
        public SignatureDataTypeEnum DataType { get; }
        public string EnumName { get; set; }
        public List<EntityLiteIntegerDto> EntityLiteDto { get; set; }
        public bool? IsRequired { get; set; }
        public string RegEx { get; set; }
        public List<string> ValidationErrorKeys { get; set; }
        public object Value { get; set; }
    }
}