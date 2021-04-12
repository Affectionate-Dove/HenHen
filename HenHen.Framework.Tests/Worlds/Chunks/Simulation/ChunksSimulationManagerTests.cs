// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Chunks.Simulation;
using HenHen.Framework.Worlds.Nodes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Tests.Worlds.Chunks.Simulation
{
    public class ChunksSimulationManagerTests
    {
        [Test]
        public void AllChunksStrategyTest()
        {
            var testSuite = new TestSuite();
            testSuite.SimulationManager.Strategy = new ChunksSimulationStrategy(ChunksSimulationStrategyType.All, null);
            var synchronizedTime = 1;
            testSuite.SimulationManager.Simulate(synchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.SimulationManager.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node11.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node12.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node13.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node21.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node22.SynchronizedTime);
        }

        [Test]
        public void SquareStrategyTest()
        {
            var testSuite = new TestSuite();
            testSuite.SimulationManager.Strategy = new ChunksSimulationStrategy(ChunksSimulationStrategyType.Square, new HashSet<ChunksSimulationRingConfiguration>
            {
                new ChunksSimulationRingConfiguration(1, 0)
            });

            var synchronizedTime = 1;
            testSuite.SimulationManager.Simulate(synchronizedTime, new Vector2(1));
            Assert.AreEqual(synchronizedTime, testSuite.SimulationManager.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node11.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node12.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node21.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node22.SynchronizedTime);

            Assert.AreEqual(0, testSuite.Node13.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node23.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node33.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node31.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node32.SynchronizedTime);
        }

        [Test]
        public void CircleStrategyTest()
        {
            var testSuite = new TestSuite();
            testSuite.SimulationManager.Strategy = new ChunksSimulationStrategy(ChunksSimulationStrategyType.Circle, new HashSet<ChunksSimulationRingConfiguration>
            {
                new ChunksSimulationRingConfiguration(1f, 0)
            });

            var synchronizedTime = 1;
            testSuite.SimulationManager.Simulate(synchronizedTime, new Vector2(1.5f));
            Assert.AreEqual(synchronizedTime, testSuite.SimulationManager.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node11.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node12.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node21.SynchronizedTime);

            Assert.AreEqual(0, testSuite.Node22.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node13.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node23.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node33.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node31.SynchronizedTime);
            Assert.AreEqual(0, testSuite.Node32.SynchronizedTime);
        }

        [Test]
        public void SimulationFrequencyTest()
        {
            var testSuite = new TestSuite();
            testSuite.SimulationManager.Strategy = new ChunksSimulationStrategy(ChunksSimulationStrategyType.Square, new HashSet<ChunksSimulationRingConfiguration>
            {
                new ChunksSimulationRingConfiguration(1f, 1),
                new ChunksSimulationRingConfiguration(2f, 2),
                new ChunksSimulationRingConfiguration(3f, 3),
            });

            var synchronizedTime = 0d;
            for (var i = 0; i < 30; i++)
            {
                synchronizedTime += 0.1;
                testSuite.SimulationManager.Simulate(synchronizedTime, new Vector2(0));
            }

            Assert.AreEqual(synchronizedTime, testSuite.SimulationManager.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node11.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node12.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node13.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node21.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node22.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node23.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node31.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node32.SynchronizedTime);
            Assert.AreEqual(synchronizedTime, testSuite.Node33.SynchronizedTime);

            Assert.AreEqual(3, testSuite.Node11.TimesSimulated);

            Assert.AreEqual(2, testSuite.Node21.TimesSimulated);
            Assert.AreEqual(2, testSuite.Node12.TimesSimulated);
            Assert.AreEqual(2, testSuite.Node22.TimesSimulated);

            Assert.AreEqual(1, testSuite.Node31.TimesSimulated);
            Assert.AreEqual(1, testSuite.Node32.TimesSimulated);
            Assert.AreEqual(1, testSuite.Node33.TimesSimulated);
            Assert.AreEqual(1, testSuite.Node13.TimesSimulated);
            Assert.AreEqual(1, testSuite.Node23.TimesSimulated);
        }

        private class TestNode : Node
        {
            public int TimesSimulated { get; private set; }

            protected override void Simulation(double duration)
            {
                base.Simulation(duration);
                TimesSimulated++;
            }
        }

        private record TestSuite
        {
            public TestNode Node11;
            public TestNode Node21;
            public TestNode Node31;
            public TestNode Node12;
            public TestNode Node22;
            public TestNode Node32;
            public TestNode Node13;
            public TestNode Node23;
            public TestNode Node33;

            public TestSuite()
            {
                ChunksManager = new ChunksManager(new Vector2(4), 1);
                SimulationManager = new ChunksSimulationManager(ChunksManager);
                ChunksManager.AddNode(Node11 = new TestNode { Position = new(1, 0, 1) });
                ChunksManager.AddNode(Node12 = new TestNode { Position = new(1, 0, 2) });
                ChunksManager.AddNode(Node13 = new TestNode { Position = new(1, 0, 3) });
                ChunksManager.AddNode(Node21 = new TestNode { Position = new(2, 0, 1) });
                ChunksManager.AddNode(Node22 = new TestNode { Position = new(2, 0, 2) });
                ChunksManager.AddNode(Node23 = new TestNode { Position = new(2, 0, 3) });
                ChunksManager.AddNode(Node31 = new TestNode { Position = new(3, 0, 1) });
                ChunksManager.AddNode(Node32 = new TestNode { Position = new(3, 0, 2) });
                ChunksManager.AddNode(Node33 = new TestNode { Position = new(3, 0, 3) });
            }

            public ChunksManager ChunksManager { get; }
            public ChunksSimulationManager SimulationManager { get; }
        }
    }
}