using CoreCodeCamper.Core.Model;
using System;
using System.Globalization;
using Xunit;

namespace UnitTests.Models
{
    public class TimeSlotTests
    {
        [Fact]
        public void FormatsToStringProperly()
        {      
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Assert.Equal("Sun 12:00 AM", new TimeSlot(new DateTime(2017, 1, 1)).ToString());
            Assert.Equal("Sun 12:00 AM", new TimeSlot(new DateTime(2017, 1, 15)).ToString());
            Assert.Equal("Wed 1:00 PM", new TimeSlot(new DateTime(2017, 2, 1, 13, 0, 0, 0)).ToString());
        }
    }
}
