using CoreCodeCamper.Core.Exceptions;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCodeCamper.Core.Model
{
    public class Room // ValueObject
    {
        public string Name { get; }

        public Room(string name) => Name = Ensure<ModelArgumentNullException>.IsNotNull(name, nameof(name));
    }
}
