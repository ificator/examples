using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ODataAnnotations
{
    public class Simple : IAnnotatable
    {
        [IgnoreDataMember]
        public Dictionary<string, object> Annotations { get; } = new Dictionary<string, object>();

        [Key]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
