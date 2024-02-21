using System.Reflection;
using System.Xml;

namespace Cms.Domain.Printers;
public class Printer<T>
{
    public void Print(List<T> items)
    {
        foreach (var item in items)
        {
            Print(item);
        }
    }
    
    public void Print(T model)
    {
        Type type = typeof(T);
        List<PropertyInfo> properties = type.GetProperties().ToList();

        foreach (var property in properties)
        {
            Console.WriteLine($"{property.Name}: {property.GetValue(model)}");
        }
        Console.WriteLine();
    }
}