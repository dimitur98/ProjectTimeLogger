using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace ProjectTimeLogger.Helpers
{
    public static class NameValueCollectionExtensions
    {
        public static NameValueCollection AddOrSet(this NameValueCollection collection, string keyName, string value)
        {
            if (collection.AllKeys.Contains(keyName)) { collection[keyName] = value; }
            else { collection.Add(keyName, value); }

            return collection;
        }

        public static string ToQueryString(this NameValueCollection collection)
        {
            var parameters = new StringBuilder();

            if (collection != null)
            {
                foreach (var key in collection.AllKeys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        var values = collection.GetValues(key);

                        if (values?.Any() == false) { continue; }

                        foreach (var value in values)
                        {
                            parameters.Append(parameters.Length == 0 ? "?" : "&");

                            parameters.Append(key);
                            parameters.Append('=');
                            parameters.Append(HttpUtility.UrlEncode(value));
                        }
                    }
                }
            }

            return parameters.ToString();
        }
    }
}