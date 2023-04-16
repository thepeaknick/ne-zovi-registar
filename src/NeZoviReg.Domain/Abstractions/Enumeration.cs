using System.Reflection;

namespace NeZoviReg.Domain.Abstractions;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
            where TEnum : Enumeration<TEnum>
{
    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    private static Dictionary<int, TEnum> _enumerations = CreateEnumerations();

    public int Id { get; protected set; }

    public string Name { get; protected set; }

    public static TEnum? FromValue(int value)
    {
        return _enumerations.TryGetValue(value, out TEnum enumeration)
                ? enumeration
                : default;
    }

    public static TEnum? FromName(string name)
    {
        return _enumerations.Values.SingleOrDefault(val => val.Name == name);
    }

    public static List<TEnum> GetValues()
    {
        return _enumerations.Values.ToList();
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
            return false;
        return GetType() == other.GetType() && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }


    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumType = typeof(TEnum);

        var fieldTypes = enumType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => enumType.IsAssignableFrom(fi.FieldType))
            .Select(fi => (TEnum)fi.GetValue(default)!);

        return fieldTypes.ToDictionary(x => x.Id);
    }
}
