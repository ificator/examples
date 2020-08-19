# Description
This example shows an approach to implement instance annotations in `Microsoft.AspNet.OData`.

It works by attaching the desired annotations to a dictionary on the returned instance, and using those values during serialization to populate the appropriate `InstanceAnnotations` collection (i.e. either on the resource or the property).

NOTE: The example forces `Prefer: odata.include-annotations=*` so that all annotations are returned by default.

# Known Issues
If you add a `$select=Name` option to the query string of the default request the instance annotations for the `Name` property are not returned. This is because `ResourceContext.ResourceInstance` does not return our instance in this case, and instead returns a new instance that is hydrated from the set of selected properties.