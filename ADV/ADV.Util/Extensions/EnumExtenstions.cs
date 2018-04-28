using System;
using System.ComponentModel;

public static class EnumExtenstions
{
    public static string GetDescriptionFromEnumValue(Type enumType, object enumValue)
    {
        try
        {
            object o = Enum.Parse(enumType, enumValue.ToString());

            string name = o.ToString();
            DescriptionAttribute[] customAttributes = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if ((customAttributes != null) && (customAttributes.Length == 1))
            {
                return customAttributes[0].Description;
            }
            return name;
        }
        catch
        {
            return "/";
        }
    }
}


