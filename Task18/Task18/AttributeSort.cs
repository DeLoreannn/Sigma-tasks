using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18
{
    enum SortOrder { Ascending, Descending }
    abstract class AttributeSort : IComparable<AttributeSort>
    {
        public string Attribute { get; set; }
        public SortOrder? SortOrder { get; set; }

        public bool Condition { get; set; }

        public AttributeSort()
        {
            Attribute = null;
            SortOrder = null;
            Condition = false;
        }

        public abstract int CompareTo(AttributeSort other);
    }
}
