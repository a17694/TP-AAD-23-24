using System.Data;

namespace InterfaceAAD.Converters;

/// <summary>
/// Helper class for converting a generic List to a DataTable.
/// </summary>
public static class DataTableConverter
{
    /// <summary>
    /// Converts a List of objects to a DataTable.
    /// </summary>
    /// <typeparam name="T">The type of objects in the List.</typeparam>
    /// <param name="items">The List of objects to be converted.</param>
    /// <returns>A DataTable containing the data from the List.</returns>
    public static DataTable ToDataTable<T>(List<T> items)
    {
        // Create a DataTable with the name of the class type
        DataTable dataTable = new DataTable(typeof(T).Name);

        // Get the properties of the class type
        var properties = typeof(T).GetProperties();

        // Add columns to the DataTable based on the properties of the class type
        foreach (var property in properties)
        {
            dataTable.Columns.Add(property.Name, property.PropertyType);
        }

        // Add rows to the DataTable with values from the List
        foreach (T item in items)
        {
            var values = new object[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(item, null);
            }

            dataTable.Rows.Add(values);
        }

        return dataTable;
    }


}