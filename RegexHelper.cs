using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class RegexHelper
{
    public static bool IsMatch(string input, string pattern)
    {
        return Regex.IsMatch(input, pattern);
    }

    
}