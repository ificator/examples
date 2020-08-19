using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataAnnotations
{
    public class SimpleSerializerProvider : DefaultODataSerializerProvider
    {
        private readonly SimpleResourceSerializer resourceSerializer;

        public SimpleSerializerProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.resourceSerializer = new SimpleResourceSerializer(this);
        }

        public override ODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType)
        {
            switch (edmType.TypeKind())
            {
                case EdmTypeKind.Complex:
                case EdmTypeKind.Entity:
                    return this.resourceSerializer;

                default:
                    return base.GetEdmTypeSerializer(edmType);
            }
        }
    }
}
