
namespace Domain.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = true)]
    public class ValidateDuplicateAttribute : System.Attribute
    {
        string Name;
        public ValidateDuplicateAttribute(string name)
        {
            Name = name;
        }

        public string GetName() => Name;
    }
}
