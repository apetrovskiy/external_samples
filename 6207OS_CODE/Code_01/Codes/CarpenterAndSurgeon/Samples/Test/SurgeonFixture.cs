using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;

namespace Samples.Test
{
    [TestFixture]
    class SurgeonFixture
    {
        [Test]
        public void CallingOperateCallsGrabOnForceps()
        {
            var forcepsMock = new Mock<Forceps>();

            var surgeon = new Surgeon(forcepsMock.Object);
            surgeon.Operate();

            forcepsMock.Verify(f => f.Grab());
        }
    }
}
