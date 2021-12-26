using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignmentONE.Helper
{
    public class CategoryFilter
    {
        public static string Filter(string s)
        {
            string[] subStrings = s.Split(',');
            for (int i = 0; i < subStrings.Length; i++)
            {
                subStrings[i] = subStrings[i].Trim();
            }
            StringBuilder newString = new StringBuilder();
            foreach (string x in subStrings)
            {
                newString.Append(x).Append(","); // eg. Music, Homey, Hunting
            }
            newString.Remove(newString.Length - 1, 1); //delecte comma
            return newString.ToString();
        }
    }
}
