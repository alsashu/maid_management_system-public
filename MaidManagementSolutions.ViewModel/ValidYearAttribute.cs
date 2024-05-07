using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidYearAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null || !(value is int))
        {
            return false;
        }

        int year = (int)value;
        return year >= 1000 && year <= 9999;
    }
}
