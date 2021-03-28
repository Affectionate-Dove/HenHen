// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;

namespace HenHen.Core.Worlds.Plants
{
    public class Flower : Plant
    {
        public int DropAmount { get; }
        public bool Collectable => GrowthStage == GrowthStagesDuration.Count;

        public Flower(FlowerBreed breed) : base(breed)
        {
            var random = new Random();
            var randomPoint = random.NextDouble();
            var currentEndPoint = 0.0;
            var suma = 0.0;
            for (var i = 0; i < breed.DropAmount.Count; i++)
            {
                suma += breed.DropAmount[i].chance;
            }
            for (var i = 0; i < breed.DropAmount.Count; i++)
            {
                currentEndPoint += (breed.DropAmount[i].chance / suma);
                if (randomPoint <= currentEndPoint)
                {
                    DropAmount = breed.DropAmount[i].amount;
                    break;
                }
            }
        }
    }
}