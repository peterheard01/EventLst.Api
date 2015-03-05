using System.Collections.Generic;

namespace EventLst.Core
{
    public interface IDtoMapper<T>
    {
        List<T> Map(dynamic json);
    }
}