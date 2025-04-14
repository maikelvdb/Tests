using System.ComponentModel;

namespace Test.Api.Configuration.Models;


//[TypeConverter(typeof(SecretValueConverter<string>))]
//public class SecretStringValue(string value) : SecretValue<string>(value) { }

//[TypeConverter(typeof(SecretValueConverter<int>))]
//public class SecretIntValue(int value) : SecretValue<int>(value) { }

//[TypeConverter(typeof(SecretValueConverter<long>))]
//public class SecretLongValue(long value) : SecretValue<long>(value) { }

//[TypeConverter(typeof(SecretValueConverter<bool>))]
//public class SecretBoolValue(bool value) : SecretValue<bool>(value) { }

//[TypeConverter(typeof(SecretValueConverter<Guid>))]
//public class SecretGuidValue(Guid value) : SecretValue<Guid>(value) { }

public class SecretValue<TValue>(TValue? value)
{
    public TValue? Value { get; } = value;

    public override string ToString() => Value?.ToString() ?? "";

    public static implicit operator SecretValue<TValue>(string input)
    {
        return new SecretValue<TValue>(GetValue<TValue>(input, typeof(TValue)));
    }

    private static T? GetValue<T>(string? value, Type type)
    {
        if (value is null)
        {
            return default!;
        }

        var result = type switch
        {
            Type t when t == typeof(string) => value as object,
            Type t when t == typeof(int) => int.Parse(value) as object,
            Type t when t == typeof(long) => long.Parse(value) as object,
            Type t when t == typeof(bool) => bool.Parse(value) as object,
            Type t when t == typeof(Guid) => Guid.Parse(value) as object,
            _ => throw new NotImplementedException()
        };

        return (T) result;
    }
}
