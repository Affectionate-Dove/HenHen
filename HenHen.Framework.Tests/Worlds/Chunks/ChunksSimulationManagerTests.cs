// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Nodes;
using NUnit.Framework;
using System.Linq;
using System.Numerics;

namespace HenHen.Framework.Tests.Worlds.Chunks
{
    public class ChunksSimulationManagerTests
    {
        [Test]
        public void SimulateSquareTest()
        {
            var chunksManager = CreateChunksManager();
            var ring1 = new SimulationRingConfiguration(0.1f, 10);
            var strategy = new ChunksSimulationStrategy(ChunksSimulationStrategyType.Square, new[]
            {
                ring1
            });

            chunksManager.SimulationManager.Strategy = strategy;

            var node1 = new TestNode { Position = new(1) };
            var node2 = new TestNode { Position = new(3.5f) };

            chunksManager.AddNode(node1);
            chunksManager.AddNode(node2);

            Assert.IsTrue(chunksManager.GetChunkForPosition(new(1)).Nodes.Contains(node1));
            Assert.IsTrue(chunksManager.GetChunkForPosition(new(3.5f)).Nodes.Contains(node2));

            var synchronizedTime = 0;

            Simulate1Second10Times(chunksManager, Vector2.Zero, ref synchronizedTime);
            Assert.AreEqual(1, node1.TimesSimulated);
            Assert.AreEqual(1, node2.TimesSimulated);
            node1.ResetTimesSimulated();
            node2.ResetTimesSimulated();

            Simulate1Second10Times(chunksManager, new(3.5f), ref synchronizedTime);
            Assert.AreEqual(1, node1.TimesSimulated);
            Assert.AreEqual(10, node2.TimesSimulated);
            node1.ResetTimesSimulated();
            node2.ResetTimesSimulated();

            Simulate1Second10Times(chunksManager, new(1), ref synchronizedTime);
            Assert.AreEqual(10, node1.TimesSimulated);
            Assert.AreEqual(1, node2.TimesSimulated);
            node1.ResetTimesSimulated();
            node2.ResetTimesSimulated();
        }

        private static void Simulate1Second10Times(ChunksManager chunksManager, Vector2 origin, ref int synchronizedTime)
        {
            for (var i = 1; i <= 10; i++)
            {
                synchronizedTime++;
                chunksManager.SimulationManager.Simulate(synchronizedTime, origin);
            }
        }

        private static ChunksManager CreateChunksManager() => new(new(10), 1);

        private class TestNode : Node
        {
            public int TimesSimulated { get; private set; }

            public void ResetTimesSimulated() => TimesSimulated = 0;

            protected override void Simulation(double duration)
            {
                base.Simulation(duration);
                TimesSimulated++;
            }
        }
    }
}