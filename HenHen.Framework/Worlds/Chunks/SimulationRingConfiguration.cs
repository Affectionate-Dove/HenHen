// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Framework.Worlds.Chunks
{
    /// <summary>
    /// Defines the frequency of simulation in a ring.
    /// </summary>
    /// <remarks>
    /// For example, with rings like this:
    /// <list type="bullet">
    /// <item>
    ///     <list type="table">
    ///     <item><see cref="InnerBoundary"/> = 3.4</item>
    ///     <item><see cref="SimulationFrequency"/> = 2 seconds</item>
    ///     </list>
    /// </item>
    /// <item>
    ///     <list type="table">
    ///     <item><see cref="InnerBoundary"/> = 10</item>
    ///     <item><see cref="SimulationFrequency"/> = 5 seconds</item>
    ///     </list>
    /// </item>
    /// </list>
    /// Chunks will be simulated like this:
    /// <list type="bullet">
    /// <item>
    ///     these within 3.4 units to <see cref="Origin"/>
    ///     will be simulated at every simulation step
    /// </item>
    /// <item>
    ///     all within 10 units (including these within 3.4 units)
    ///     will be simulated every 2 seconds
    /// </item>
    /// <item>
    ///     outside 10 units will be simulated every 5 seconds
    /// </item>
    /// </list>
    /// </remarks>
    public record SimulationRingConfiguration
    {
        public SimulationRingConfiguration(float innerBoundary, float simulationFrequency)
        {
            InnerBoundary = innerBoundary;
            SimulationFrequency = simulationFrequency;
        }

        public float InnerBoundary { get; }
        public float SimulationFrequency { get; }
    }
}