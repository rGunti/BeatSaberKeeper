using System;
using BeatKeeper.App.Utils.Updater;
using BeatSaberKeeper.Updater;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeatSaberKeeper.Tests.Updater
{
    [TestClass]
    public class BskVersionTests
    {
        [TestMethod]
        [DataRow("1.2.34-alpha+021abfe", 1, 2, 34, "alpha", "021abfe")]
        [DataRow("1.2.34+021abfe", 1, 2, 34, "", "021abfe")]
        [DataRow("1.2.34-alpha", 1, 2, 34, "alpha", "")]
        [DataRow("1.2.34", 1, 2, 34, "", "")]
        public void CanParseVersionCorrectly(
            string input,
            int expectedMajor,
            int expectedMinor,
            int expectedRevision,
            string expectedSuffix,
            string expectedCommit)
        {
            BskVersion parsed = BskVersion.Parse(input);

            Assert.AreEqual(expectedMajor, parsed.Major);
            Assert.AreEqual(expectedMinor, parsed.Minor);
            Assert.AreEqual(expectedRevision, parsed.Revision);
            Assert.AreEqual(expectedSuffix, parsed.Suffix);
            Assert.AreEqual(expectedCommit, parsed.Commit);

            Assert.AreEqual(input, parsed.ToString());
        }

        private void DoBooleanOperatorTest(
            string inputA, string inputB,
            bool expectedResult,
            Func<BskVersion, BskVersion, bool> operatorFn,
            Func<string, string, bool> inputDataCheckFn = null,
            string failureText = null)
        {
            BskVersion parsedA = BskVersion.Parse(inputA),
                parsedB = BskVersion.Parse(inputB);

            if (inputDataCheckFn != null)
            {
                Assert.AreEqual(expectedResult, inputDataCheckFn(inputA, inputB),
                    "Input data didn't qualify for expected outcome!");
            }

            Assert.AreEqual(expectedResult, operatorFn(parsedA, parsedB),
                failureText ?? string.Empty);
        }

        [TestMethod]
        [DataRow("1.2.34-alpha+d3adb33f", "1.2.34-alpha+d3adb33f", true)]
        [DataRow("1.2.34-alpha", "1.2.34-alpha+d3adb33f", false)]
        public void EqualityOperatorWorks(
            string inputA,
            string inputB,
            bool expectToBeEqual)
        {
            DoBooleanOperatorTest(
                inputA, inputB, expectToBeEqual,
                (a, b) => a == b,
                (a, b) => a == b);
        }

        [TestMethod]
        [DataRow("1.2.34-alpha+d3adb33f", "1.2.34", false)]
        [DataRow("1.2.35-alpha+d3adb33f", "1.2.34", true)]
        public void IsGreaterWorks(
            string inputA,
            string inputB,
            bool expectedResult)
        {
            var version = new Version();
            DoBooleanOperatorTest(
                inputA, inputB, expectedResult,
                (a, b) => a > b);
        }

        [TestMethod]
        [DataRow("1.2.34-alpha", "1.2.34-beta", true)]
        [DataRow("1.2.34-beta", "1.2.34-alpha", false)]
        [DataRow("1.2.34-beta", "1.2.34-rc", true)]
        [DataRow("1.2.34-derp", "1.2.34-rc", true)]
        [DataRow("1.2.34-rc", "1.2.34-derp", false)]
        public void IsLessWorks(string inputA, string inputB, bool expectedResult)
        {
            DoBooleanOperatorTest(inputA, inputB, expectedResult,
                (a, b) => a < b);
        }

        [TestMethod]
        [DataRow("1.0.0", "1.0.0", BskVersionEqualityLevel.Exact)]
        [DataRow("1.0.0", "1.0.0", BskVersionEqualityLevel.Commit)]
        [DataRow("1.0.0", "1.0.0", BskVersionEqualityLevel.Stage)]
        [DataRow("1.0.0", "1.0.0", BskVersionEqualityLevel.Revision)]
        [DataRow("1.0.0", "1.0.0", BskVersionEqualityLevel.Minor)]
        [DataRow("1.0.0", "1.0.0", BskVersionEqualityLevel.Major)]
        [DataRow("1.0.0", "1.0.0-beta", BskVersionEqualityLevel.Revision)]
        public void SamenessWorks(string inputA, string inputB, BskVersionEqualityLevel level)
        {
            Action<BskVersionEqualityLevel> doTest = l => DoBooleanOperatorTest(inputA, inputB, true,
                (a, b) => a.IsSameAs(b, l));

            switch (level)
            {
                case BskVersionEqualityLevel.Exact:
                    doTest(BskVersionEqualityLevel.Exact);
                    goto case BskVersionEqualityLevel.Commit;
                case BskVersionEqualityLevel.Commit:
                    doTest(BskVersionEqualityLevel.Commit);
                    goto case BskVersionEqualityLevel.Stage;
                case BskVersionEqualityLevel.Stage:
                    doTest(BskVersionEqualityLevel.Stage);
                    goto case BskVersionEqualityLevel.Revision;
                case BskVersionEqualityLevel.Revision:
                    doTest(BskVersionEqualityLevel.Revision);
                    goto case BskVersionEqualityLevel.Minor;
                case BskVersionEqualityLevel.Minor:
                    doTest(BskVersionEqualityLevel.Minor);
                    goto case BskVersionEqualityLevel.Major;
                case BskVersionEqualityLevel.Major:
                    doTest(BskVersionEqualityLevel.Major);
                    break;
                default:
                    Assert.Fail($"Level {level} is not tested");
                    break;
            }
        }
    }
}