using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfClient.Extensions
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> tgtCollection)
        {
            // Create observable collection
            var obCollection = new ObservableCollection<T>();

            // translate target collection into teh observable collection
            foreach (var item in tgtCollection)
            {
                obCollection.Add(item);
            }

            return obCollection;
        }
    }
}
