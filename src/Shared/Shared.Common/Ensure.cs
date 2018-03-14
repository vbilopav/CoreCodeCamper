using System;

namespace Shared.Common
{
    /// <summary>
    /// 
    ///     using static Ensure<MyCustomModelArgumentNullException>;
    ///     ...
    ///     Assign or throw custom exception:
    ///     firstName = IsNotNull(firstName, nameof(firstName));
    /// 
    ///     *** This will be obsolete in c# 8.0 ***
    /// </summary>
    public class Ensure<E> where E : Exception, new()
    {
        private static void Throw(string message = default)
        {
            if (message == default)
            {
                throw new E();
            }
            throw (E)Activator.CreateInstance(typeof(E), message);
        }

        public static T Condition<T>(T argument, Func<T, bool> condition, string message = default)
        {
            if (!condition(argument))
            {
                Throw(message);
            }
            return argument;
        }

        public static T IsNotNull<T>(T argument, string message = default) 
            => Condition(argument, (a) => a != null, message);
    }    
}
