using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ODataAnnotations
{
    public class Simple
    {
        [DataMember]
        public Complex Complex { get; set; }

        [Key]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }

    public class Complex
    {
        public string Value { get; set; }
    }
}
