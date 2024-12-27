using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Solutionhead.Data.EntityFramework
{
    public static class ObjectKeyExtractor<TObject> where TObject : class
    {
        public static string ExtractKey(TObject objectToExtractKeyFrom)
        {
            var keys = new List<string>();
            var properties = typeof(TObject).GetProperties();
            var keyProperties = properties.Where(prop => prop.GetCustomAttributes(typeof(KeyAttribute), true).Length > 0);
            SortByColumnOrderIfNecessary(ref keyProperties);

            if (!AddPropertiesToArray(keyProperties, ref keys, objectToExtractKeyFrom))
            {
                if (!AddPropertiesToArray(properties.Where(prop => prop.Name.ToLower() == "id"), ref keys, objectToExtractKeyFrom))
                {
                    AddPropertiesToArray(properties.Where(prop => prop.Name.ToLower() == typeof(TObject).Name.ToLower() + "id"), ref keys, objectToExtractKeyFrom);
                }
            }

            return string.Join(":", keys);
        }

        private static void SortByColumnOrderIfNecessary(ref IEnumerable<PropertyInfo> keyProperties)
        {
            var keyPropertiesList = keyProperties.ToList();

            if (keyPropertiesList.Count() > 1)
            {
                keyProperties = keyPropertiesList
                    .OrderBy(prop => ((ColumnAttribute)prop.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault()).Order);
            }
        }

        private static bool AddPropertiesToArray(IEnumerable<PropertyInfo> properties, ref List<string> array, TObject objectToAddValueFrom)
        {
            var propertiesList = properties.ToList();

            if (propertiesList.Any())
            {
                foreach (var property in propertiesList)
                {
                    array.Add(property.GetValue(objectToAddValueFrom, null).ToString());
                }

                return true;
            }

            return false;
        }
    }
}
