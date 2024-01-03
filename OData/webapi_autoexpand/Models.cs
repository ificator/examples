using Microsoft.AspNet.OData.Builder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapi_autoexpand
{
    public class Root
    {
        [Key]
        public string Id { get; set; }

        [AutoExpand]
        [Contained]
        public IEnumerable<Expandable1> E1s { get; set; }

        [AutoExpand]
        [Contained]
        public IEnumerable<Expandable2> E2s { get; set; }
    }

    public class Expandable1
    {
        [Key]
        public string Id { get; set; }
    }

    public class Expandable2
    {
        [Key]
        public string Id { get; set; }

        [AutoExpand]
        [Contained]
        public IEnumerable<Expandable1> E1s { get; set; }

        [Contained]
        public IEnumerable<Expandable3> E3s { get; set; }
    }

    public class Expandable3
    {
        [Key]
        public string Id { get; set; }

        [AutoExpand]
        [Contained]
        public IEnumerable<Expandable1> E1s { get; set; }
    }

}
