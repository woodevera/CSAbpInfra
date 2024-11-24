namespace CSAbpInfra.Application.Contracts.Dto.Shared
{
    public class SignatureDto
    {
        public SignatureDto(string name)
        {
            Name = name;
            Fields = new List<FieldSignatureDto>();
        }
        public List<FieldSignatureDto> Fields { get; }
        public string Name { get; }
    }
}