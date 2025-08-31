namespace Warehouse.Api.src.Infrastructure.Providers.Helpers;

public static class Methods
{
    public static bool HasNull<T>(this T obj)
    {
        var props = typeof(T).GetProperties();
        for (var idx = 0; idx < props.Length; idx++)
        {
            var t = props[idx];
            if (Nullable.GetUnderlyingType(t.PropertyType) != null) continue;
            if (t.GetValue(obj) == null)
                return true;
        }
        return false;
    }
}
