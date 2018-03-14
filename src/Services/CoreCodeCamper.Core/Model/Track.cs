using CoreCodeCamper.Core.Exceptions;
using Shared.Common;

namespace CoreCodeCamper.Core.Model
{
    public class Track // ValueObject
    {
        public string Name { get; }

        public Track(string name) => Name = Ensure<ModelArgumentNullException>.IsNotNull(name, nameof(name));
    }
}
