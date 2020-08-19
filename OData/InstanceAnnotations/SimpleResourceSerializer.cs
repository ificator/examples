using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.OData;
using System.Collections.Generic;

namespace ODataAnnotations
{
    public class SimpleResourceSerializer : ODataResourceSerializer
    {
        public SimpleResourceSerializer(ODataSerializerProvider serializerProvider) : base(serializerProvider)
        {
        }

        public override ODataResource CreateResource(SelectExpandNode selectExpandNode, ResourceContext resourceContext)
        {
            ODataResource resource = base.CreateResource(selectExpandNode, resourceContext);

            if (resourceContext.ResourceInstance is IAnnotatable annotatable)
            {
                foreach (var annotation in annotatable.Annotations)
                {
                    int atIndex = annotation.Key.IndexOf('@');
                    ICollection<ODataInstanceAnnotation> instanceAnnotations = null;

                    if (atIndex <= 0)
                    {
                        instanceAnnotations = resource.InstanceAnnotations;
                    }
                    else
                    {
                        string propertyName = annotation.Key.Substring(0, atIndex);
                        foreach (var property in resource.Properties)
                        {
                            if (property.Name == propertyName)
                            {
                                instanceAnnotations = property.InstanceAnnotations;
                            }
                        }
                    }

                    if (instanceAnnotations != null)
                    {
                        instanceAnnotations.Add(
                            new ODataInstanceAnnotation(
                                annotation.Key.Substring(atIndex + 1),
                                new ODataPrimitiveValue(annotation.Value)));
                    }
                }
            }

            return resource;
        }
    }
}
