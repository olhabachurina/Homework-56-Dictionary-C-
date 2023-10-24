using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_56_Dictionary_C_
{
    internal interface IDictionaryOperation
    {
        void Execute(Dictionary<string, List<string>> dictionary);
    }
}
