// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Core.Worlds.Plants
{
    public class Flower : Plant
    {
        public override bool Collectable => !Disappearing && GrowthStage == GrowthStagesDuration.Count;

        public Flower(FlowerBreed breed, HenHenTime birthDate) : base(breed, birthDate) => DropAmount = breed.DropAmount.GetRandom();

        protected override void AfterFruitsDrop()
        {
            base.AfterFruitsDrop();
            Disappear();
        }
    }
}