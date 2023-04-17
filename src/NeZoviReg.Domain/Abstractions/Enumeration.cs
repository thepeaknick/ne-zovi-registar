using System.Reflection;

namespace NeZoviReg.Domain.Abstractions;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
            where TEnum : Enumeration<TEnum>
{
    protected Enumeration()
    {
    }

    protected Enumeration(int id, string name)
       :this()
    {
        Id = id;
        Name = name;
    }

    private static readonly Lazy<Dictionary<int, TEnum>> _enumerationsDictionary =
        new(() => CreateEnumerationDictionary(typeof(TEnum)));

    public int Id { get; protected set; }

    public string Name { get; protected set; }

    public static TEnum? FromValue(int value)
    {
        return _enumerationsDictionary.Value.TryGetValue(value, out TEnum enumeration)
                ? enumeration
                : default;
    }

    public static TEnum? FromName(string name)
    {
        return _enumerationsDictionary.Value.Values.SingleOrDefault(val => val.Name == name);
    }

    public static bool Contains(int id) => _enumerationsDictionary.Value.ContainsKey(id);

    public static IList<TEnum> GetValues() => _enumerationsDictionary.Value.Values.ToList();

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
        return Id.GetHashCode() * 37;
    }


    private static Dictionary<int, TEnum> CreateEnumerationDictionary(Type enumType) => GetFieldsForType(enumType).ToDictionary(t => t.Id);

    private static IEnumerable<TEnum> GetFieldsForType(Type enumType) =>
        enumType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);
}
