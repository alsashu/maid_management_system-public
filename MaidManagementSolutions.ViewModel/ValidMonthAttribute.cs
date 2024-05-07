using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidMonthAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null || !(value is int))
        {
            return false;
        }

        int month = (int)value;
        return month >= 1 && month <= 12;
    }
}
