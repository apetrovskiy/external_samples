using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMMAutoProject;

namespace SpecWrap02
{
    public static class TestsPrerequisites
    {
        public static DomainWorker sdom = new DomainWorker("studyroot\\administrator", "123", "studyrootdc");
        public static DomainWorker tdom = new DomainWorker("studynewr2\\administrator", "123", "studynewr2dc");
        public static LegacyQMM.DSAWorker dsaWorker = new LegacyQMM.DSAWorker("spb8528", 389, "CN=legacyQMM", "spb8528\\drulev", "");
    }
}
