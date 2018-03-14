using CoreCodeCamper.Core.Exceptions;
using CoreCodeCamper.Core.Model;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCodeCamper.Core.Model
{
    using static Ensure<ModelArgumentNullException>;

    public class Session // Entity, Aggregate root
    {
        public string Code { get; }
        public string Title { get; }
        public string Description { get; }
        public TimeSlot TimeSlot { get; }
        public Track Track { get;  }
        public Room Room { get; }
        public Person Speaker { get; }

        public Session(string title, string description, string code, TimeSlot timeSlot, Track track, Room room, Person speaker)
            => (Code, Title, Description, TimeSlot, Track, Room, Speaker) = (
                IsNotNull(code, nameof(code)),
                IsNotNull(title, nameof(title)),
                IsNotNull(description, nameof(description)),
                IsNotNull(timeSlot, nameof(timeSlot)),
                IsNotNull(track, nameof(track)),
                IsNotNull(room, nameof(room)),
                speaker
            );
    }
}