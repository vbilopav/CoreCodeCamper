using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCodeCamper.Core.Exceptions
{
    public class ModelArgumentNullException : ArgumentException
    {
        private readonly string argumentName;

        public ModelArgumentNullException(string argumentName) : base($"{argumentName} cannot be null.")
        {
            this.argumentName = argumentName;
        }

        public ModelArgumentNullException() { }
    }
}
