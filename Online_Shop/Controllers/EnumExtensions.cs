﻿using System.ComponentModel;
using System.Reflection;

namespace Bags_Wallets
{
    public static class EnumExtensions
    {
        public static string DisplayName(this Enum value)
        {
            if (value == null)
            {
                return null;
            }
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])field
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }
    }
}
