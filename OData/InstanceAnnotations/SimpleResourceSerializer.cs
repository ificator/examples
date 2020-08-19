using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.OData;

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
                    if (atIndex <= 0)
                    {
                        resource.InstanceAnnotations.Add(
                            new ODataInstanceAnnotation(
                                annotation.Key.Substring(atIndex + 1),
                                new ODataPrimitiveValue(annotation.Value)));
                    }
                }
            }

            return resource;
        }

        public override void AppendDynamicProperties(ODataResource resource, SelectExpandNode selectExpandNode, ResourceContext resourceContext)
        {
            base.AppendDynamicProperties(resource, selectExpandNode, resourceContext);

            if (resourceContext.ResourceInstance is IAnnotatable annotatable)
            {
                foreach (var annotation in annotatable.Annotations)
                {
                    int atIndex = annotation.Key.IndexOf('@');
                    if (atIndex > 0)
                    {
                        string propertyName = annotation.Key.Substring(0, atIndex);
                        foreach (var property in resource.Properties)
                        {
                            if (property.Name == propertyName)
                            {
                                property.InstanceAnnotations.Add(
                                    new ODataInstanceAnnotation(
                                        annotation.Key.Substring(atIndex + 1),
                                        new ODataPrimitiveValue(annotation.Value)));
                            }
                        }
                    }
                }
            }
        }
    }
}
