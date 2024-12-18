using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebApp.Models;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        if (enumValue == null)
            throw new ArgumentNullException(nameof(enumValue));
        var member = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault();

        if (member != null)
        {
            var displayAttribute = member.GetCustomAttribute<DisplayAttribute>();
            
            if (displayAttribute != null)
            {
                return displayAttribute.GetName();
            }
        }
        
        return enumValue.ToString();
    }
}