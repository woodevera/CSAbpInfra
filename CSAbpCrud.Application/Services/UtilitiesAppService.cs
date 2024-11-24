using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CSAbpCrud.Application.Contracts.Dto.Shared;
using CSAbpCrud.Application.Contracts.Enums;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace CSAbpCrud.Application.Services
{
    /// <summary>
    /// UtilitiesAppService
    /// Created By : Mahmoud Radwan
    /// Created On : 28-01-2023
    /// </summary>
    public class UtilitiesAppService : ApplicationService , ISingletonDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public UtilitiesAppService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        

        /// <summary>
        /// Function used to Get Dto Signature ( used for frond end Crud components )
        /// Created By : Mahmoud Radwan
        /// Created On : 28-01-2023
        /// </summary>
        public async Task<SignatureDto> GetDtoSignatureAsync(string dtoName)
        {
            /* load required dto */
            if (dtoName is null)
            {
                throw new BusinessException("DtoNotFound");
            }
            var dtoType = CSUtility.GetTypeByName(dtoName);
            if (dtoType is null)
            {
                throw new BusinessException("DtoNotFound");
            }

            var result = new SignatureDto(dtoName);
            
            /* get dto properties */
            foreach (var item in dtoType.GetProperties())
            {
                var fieldType = GetPropertyDataType(item);
                if (fieldType == SignatureDataTypeEnum.UnKnown)
                {
                    continue;
                }
                
                var field = new FieldSignatureDto(item.Name.FirstCharToLowerCase(), fieldType);
                if (field.DataType == SignatureDataTypeEnum.Enum)
                {
                    /* load enum name */
                    field.EnumName = item.PropertyType.Name;
                    field.EntityLiteDto = GetEntityLiteDtoFromEnum(item.PropertyType);
                }
                
                /* load validation rules */
                var requiredAttribute = item.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttribute is not null)
                {
                    field.IsRequired = true;
                    field.ValidationErrorKeys ??= new List<string>();
                    field.ValidationErrorKeys.Add(requiredAttribute.ErrorMessage);
                }

                var regexAttribute = item.GetCustomAttribute<RegularExpressionAttribute>();
                if (regexAttribute is not null)
                {
                    field.RegEx = regexAttribute.Pattern;
                    field.ValidationErrorKeys ??= new List<string>();
                    field.ValidationErrorKeys.Add(regexAttribute.ErrorMessage);
                }
                result.Fields.Add(field);
            }
            
            return result;
        }

        private List<EntityLiteIntegerDto> GetEntityLiteDtoFromEnum(Type itemPropertyType)
        {
            List<EntityLiteIntegerDto> result = new List<EntityLiteIntegerDto>();
            foreach (var item in Enum.GetValues(itemPropertyType))
            {
                result.Add(new EntityLiteIntegerDto()
                {
                    Id =  item.To<int>(),
                    Label = Enum.GetName(itemPropertyType , item)
                });
            }
            return result;
        }

        private SignatureDataTypeEnum GetPropertyDataType(PropertyInfo propertyInfo)
        {

            /* int types */
            var intTypes = new List<Type>()
            {
                typeof(int),
                typeof(byte),
                typeof(long),
                typeof(short),
                typeof(uint),
                typeof(ulong),
                typeof(ushort),
                typeof(sbyte),
                typeof(int?),
                typeof(byte?),
                typeof(long?),
                typeof(short?),
                typeof(uint?),
                typeof(ulong?),
                typeof(ushort?),
                typeof(sbyte?),
            };
            
            var decimalTypes = new List<Type>()
            {
                typeof(double),
                typeof(float),
                typeof(decimal),
                typeof(double?),
                typeof(float?),
                typeof(decimal?)
            };

            if (propertyInfo.PropertyType.IsEnum)
            {
                return SignatureDataTypeEnum.Enum;
            }

            if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(Boolean) || propertyInfo.PropertyType == typeof(bool?))
            {
                return SignatureDataTypeEnum.Boolean;
            }
            
            if (intTypes.Contains(propertyInfo.PropertyType))
            {
                return SignatureDataTypeEnum.Integer;
            }

            if (decimalTypes.Contains(propertyInfo.PropertyType))
            {
                return SignatureDataTypeEnum.Decimal;
            }

            if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
            {
                return SignatureDataTypeEnum.Date;
            }

            if (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(String))
            {
                return SignatureDataTypeEnum.String;
            }

            return SignatureDataTypeEnum.UnKnown;
        }
    }
}