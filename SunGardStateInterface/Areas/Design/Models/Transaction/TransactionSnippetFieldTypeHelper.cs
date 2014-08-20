using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;

namespace StateInterface.Areas.Design.Models
{
    public static class TransactionSnippetFieldTypeHelper
    {
        /// <summary>
        /// Returns a key value pair of string name to match to an enumeration and separate string description
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string,string>> TypeValues()
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string,string>("Text", "Text"),
                new KeyValuePair<string,string>("Numeric", "Numeric"),
                new KeyValuePair<string,string>("Date", "Date"),
                new KeyValuePair<string,string>("SSN", "SSN"),
                new KeyValuePair<string,string>("Counter", "Counter"),
                new KeyValuePair<string,string>("Name", "Name"),
                new KeyValuePair<string,string>("Convert", "Convert"),
                new KeyValuePair<string,string>("SystemDate", "System Date"),
                new KeyValuePair<string,string>("StateProvinceRegion", "State / Province / Region")
            };
        }
    }
}