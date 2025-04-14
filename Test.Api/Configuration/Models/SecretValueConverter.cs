using System.ComponentModel;
using System.Globalization;

namespace Test.Api.Configuration.Models;

public class SecretValueConverter<T> : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        try
        {
            return TryParse(value?.ToString(), typeof(T), out var parsed) 
                ? parsed 
                : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }

        //return value is string s
        //    ? typeof(T) switch
        //    {
        //        Type t when t == typeof(string) => new SecretStringValue(s),
        //        Type t when t == typeof(int) => new SecretIntValue(int.Parse(s)),
        //        Type t when t == typeof(long) => new SecretLongValue(long.Parse(s)),
        //        Type t when t == typeof(bool) => new SecretBoolValue(bool.Parse(s)),
        //        Type t when t == typeof(Guid) => new SecretGuidValue(Guid.Parse(s)),
        //        _ => throw new NotSupportedException(),
        //    }
        //    : base.ConvertFrom(context, culture, value);
    }

    private static bool TryParse(string? value, Type type, out T? result)
    {
        result = default!;

        if (value is null)
        {
            return false;
        }

        try
        {
            var parsed = type switch
            {
                Type t when t == typeof(string) => value as object,
                Type t when t == typeof(int) => int.Parse(value) as object,
                Type t when t == typeof(long) => long.Parse(value) as object,
                Type t when t == typeof(bool) => bool.Parse(value) as object,
                Type t when t == typeof(Guid) => Guid.Parse(value) as object,
                _ => throw new NotImplementedException()
            };

            result = (T?)parsed;

            return true;
        }
        catch (NotImplementedException)
        {
            result = default!;
            return false;
        }
    }
}
