// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Core.Worlds.Plants
{
    public class Flower : Plant
    {
        public int DropAmount { get; }
        public bool Collectable => GrowthStage == GrowthStagesDuration.Count;

        public Flower(FlowerBreed breed) : base(breed) => DropAmount = breed.DropAmountChanceTable.GetRandom();
    }
}