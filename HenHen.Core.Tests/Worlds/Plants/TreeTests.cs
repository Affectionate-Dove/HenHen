// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using HenHen.Core.Worlds;
using HenHen.Core.Worlds.Plants;
using NUnit.Framework;

namespace HenHen.Core.Tests.Worlds.Plants
{
    public class TreeTests
    {
        private TreeBreed treeBreed;
        private Tree tree;

        [SetUp]
        public void SetUp()
        {
            treeBreed = new TreeBreed("Some tree breed", new RandomHenHenTimeRange[]
            {
                new(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(2)),
                new(HenHenTime.FromSeconds(3), HenHenTime.FromSeconds(3)),
            }, 4, new(HenHenTime.FromSeconds(3), HenHenTime.FromSeconds(3)), new[] { "Spring" });

            tree = new Tree(treeBreed, new());
        }
    }
}