// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;

namespace HenHen.Framework.Worlds.Chunks
{
    public class SimulationRingInfo : IComparable<SimulationRingInfo>
    {
        public float InnerBoundary { get; }
        public double SimulationFrequency { get; }
        public double RingTime { get; private set; }

        public SimulationRingInfo(SimulationRingConfiguration configuration)
        {
            InnerBoundary = configuration.InnerBoundary;
            SimulationFrequency = configuration.SimulationFrequency;
        }

        public void OnSimulation(double newGlobalTime) => RingTime = newGlobalTime;

        public bool ShouldBeSimulated(double globalTime) => globalTime >= RingTime + SimulationFrequency;

        public int CompareTo(SimulationRingInfo other) => Math.Sign(InnerBoundary - other.InnerBoundary);
    }
}