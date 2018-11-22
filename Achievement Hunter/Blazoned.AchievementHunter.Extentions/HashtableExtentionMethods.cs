using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.Extentions
{
    public static class HashtableExtentionMethods
    {
        #region Extention Methods
        /// <summary>
        /// Adds the specified key and value into the Hashtable only if the item is not already in the collection.
        /// </summary>
        /// <param name="source">The source Hashtable to which to add the item.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        /// <returns>Returns false if the key already exist within the Hashtable.</returns>
        public static bool SafeAdd(this Hashtable source, object key, object value)
        {
            bool success;

            if (success = !source.ContainsKey(key))
                source.Add(key, value);

            return success;
        }

        /// <summary>
        /// Adds the keys and respective values of the specified collection to the end of the Hashtable. Items with a duplicate key will be skipped.
        /// </summary>
        /// <param name="source">The source Hashtable to which to add the item.</param>
        /// <param name="collection">The collection whose keys and respective values should be added to the end of the Hashtable. The collection itself cannot be null, but it can contain keys with their respective value being null.</param>
        public static void AddRange(this Hashtable source, Hashtable collection)
        {
            if (collection == null)
                throw new ArgumentNullException("The collection is empty.");
            
            foreach (DictionaryEntry item in collection)
            {
                source.SafeAdd(item.Key, item.Value);
            }

            List<string> apple = new List<string>();
        }
        #endregion
    }
}
