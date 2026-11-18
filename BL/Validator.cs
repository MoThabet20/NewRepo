using Domains;
using Domains.CustomAttributes;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BL
{
    public class Validator<T> : IValidator<T> where T : BaseEntity
    {
        public bool Validate(T model)
        {

            //check model != null
            if (model == null) return false;
            var modelType = model.GetType();
            var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (model.Id < 0)
                return false;
            foreach (var property in modelProperties)
            {
                if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    continue;
                var propertyValue = property.GetValue(model);

                if (property.PropertyType == typeof(Int32))
                {
                    if (propertyValue != null && (int)propertyValue < 0)
                        return false;
                }

                else if (property.PropertyType == typeof(double))
                {
                    if (propertyValue != null && (double)propertyValue < 0)
                        return false;
                }

                else if (property.PropertyType == typeof(decimal))
                {
                    if (propertyValue != null && (decimal)propertyValue < 0)
                        return false;
                }

                else if (property.PropertyType == typeof(string))
                {
                    if (propertyValue != null && string.IsNullOrWhiteSpace((string)propertyValue))
                        return false;
                }

                else if (property.PropertyType == typeof(DateTime))
                {
                    if (propertyValue != null && (DateTime)propertyValue == default)
                        return false;
                }

                //===============================
                // Attribute checks
                //=================================

                // [Required]
                if (Attribute.IsDefined(property, typeof(RequiredAttribute)))
                {
                    if (propertyValue == null)
                        return false;
                    if (property.PropertyType == typeof(string) && String.IsNullOrWhiteSpace((string)propertyValue)) ;
                }

                // [minLength] [maxlength] [Email] Name
                if (property.PropertyType == typeof(string))
                {
                    var str = (string)propertyValue;
                    var minLength = property.GetCustomAttribute<MinLengthAttribute>();
                    var maxLength = property.GetCustomAttribute<MaxLengthAttribute>();
                    if (minLength != null && str.Length < minLength.Length)
                        return false;

                    //if (maxLength != null && str.Length > maxLength.Length)
                        //return false;

                    //Email validation
                    if (Attribute.IsDefined(property, typeof(EmailAttribute)))
                    {
                        //regular expression for email validation
                        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                        if (!Regex.IsMatch(str, emailPattern))
                            return false;
                    }

                    //phone validation
                    if (Attribute.IsDefined(property, typeof(PhoneAttribute)))
                    {
                        var phoneAttr = property.GetCustomAttribute<PhoneAttribute>();
                        if (phoneAttr != null && str.Length != phoneAttr.Length)
                           return false;
                    }

                }

                // [Range]
                var rangeAttr = property.GetCustomAttribute<RangeAttribute>();
                if (rangeAttr != null)
                {
                    try
                    {
                        var numberValue = Convert.ToDecimal(propertyValue);
                        if (numberValue < rangeAttr.Min || numberValue > rangeAttr.Max)
                            return false;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (e.g., log it) or rethrow it
                        throw new InvalidOperationException($"Error validating property '{property.Name}': {ex.Message}", ex);
                    }
                }
            }
            return true;
        }
    }
}
