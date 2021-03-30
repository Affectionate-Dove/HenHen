// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Random;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HenHen.Framework.Tests.Random
{
    public class ChanceTableTests
    {
        [Test]
        public void GetRandomTest()
        {
            var chanceTable = new ChanceTable<int>(new List<ChanceTableEntry<int>>
            {
                new ChanceTableEntry<int>(1,2),
                new ChanceTableEntry<int>(2,4),
                new ChanceTableEntry<int>(3,3)
            });
            var values = new List<int>();
            for (var i = 0; i < 1000; i++)
            {
                values.Add(chanceTable.GetRandom());
            }
            Assert.IsTrue(values.Contains(1));
            Assert.IsTrue(values.Contains(2));
            Assert.IsTrue(values.Contains(3));
            var amountOf1 = values.Count(value => value == 1);
            var amountOf2 = values.Count(value => value == 2);
            var amountOf3 = values.Count(value => value == 3);
            Assert.IsTrue(amountOf1 < amountOf3);
            Assert.IsTrue(amountOf3 < amountOf2);
            Console.WriteLine("1: " + amountOf1);
            Console.WriteLine("2: " + amountOf2);
            Console.WriteLine("3: " + amountOf3);
        }
    }
}