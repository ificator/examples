using System.Collections.Generic;

namespace ODataAnnotations
{
    public interface IAnnotatable
    {
        Dictionary<string, object> Annotations { get; }
    }
}
