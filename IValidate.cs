using System.Collections.Generic;

namespace TaskFromBCS
{
    interface IValidate
    {
        public bool GetValidate(IEnumerable<string> data, IWordComparator comparator);
    }
}
