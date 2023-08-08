using Microsoft.OpenApi.Any;
using Restfull.DTO.User;

namespace Restfull.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class DTOAttribute:Attribute
    {
        public readonly object? Create;
        public readonly object? Update;
        public DTOAttribute(object? create, object? update)
        {
            Create = create;
            Update = update;
        }

    }

}
