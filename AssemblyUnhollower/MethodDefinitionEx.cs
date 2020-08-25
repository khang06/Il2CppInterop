using System.Globalization;
using System.Linq;
using Mono.Cecil;

namespace AssemblyUnhollower
{
    public static class MethodDefinitionEx
    {
        public static long ExtractAddress(this MethodDefinition originalMethod)
        {
            var addressAttribute = originalMethod.CustomAttributes.SingleOrDefault(it => it.AttributeType.Name == "AddressAttribute");
            var rvaField = addressAttribute?.Fields.SingleOrDefault(it => it.Name == "Offset");

            if (rvaField?.Name == null) return 0;

            var addressString = (string) rvaField.Value.Argument.Value;
            long.TryParse(addressString.Substring(2), NumberStyles.HexNumber, null, out var address);
            return address;
        }
    }
}