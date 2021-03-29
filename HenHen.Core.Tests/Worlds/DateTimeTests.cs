// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Worlds;
using NUnit.Framework;

namespace HenHen.Core.Tests.Worlds
{
    public class DateTimeTests
    {
        [TestCase(6, 1)]
        [TestCase(19, 3)]
        public void YearsTest(int years, int expected)
        {
            var year = new DateTime
            {
                Years = years
            };
            Assert.AreEqual(expected, year.Years);
        }
    }
}