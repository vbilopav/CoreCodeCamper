using CoreCodeCamper.Core.Exceptions;
using Shared.Common;

namespace CoreCodeCamper.Core.Model
{
    using static Ensure<ModelArgumentNullException>;

    public class Person // Entity
    {       
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName => $"{FirstName} {LastName}";

        public string Image { get; }
        public string Email { get; }        
        public string Bio { get; }

        public Person(int id, string firstName, string lastname, string image, string email, string bio)
            => (Id, FirstName, LastName, Image, Email, Bio) = (
                id, 
                IsNotNull(firstName, nameof(firstName)), 
                IsNotNull(lastname, nameof(lastname)),
                IsNotNull(image, nameof(image)),
                IsNotNull(email, nameof(email)),
                IsNotNull(bio, nameof(bio)));        
    }
}
