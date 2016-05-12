using System.Collections.Generic;

namespace AutoRift.Data
{
    public interface IBuild<T> : IEnumerable<T>
    {
        T[] Items { get; set; }
    }
}