// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Numerics;

namespace HenHen.Framework.Extensions
{
    public static class VectorExtensions
    {
        private const double radiansMultiplier = 0.017453292519943295769236907684886127134428718885417254560971914401710091146034494436822415696345094822123044925073790592483854692275281012398474218934047117319168245015010769561697553581238605305168789;

        public static float GetAngle(this Vector2 v)
        {
            var angle = (float)(57.295779513082320876798154814105170332405472466564321549160243861202847148321552632440968995851110944186223381632864893281448264601248315036068267863411942122526388097467267926307988702893110767938261 * Math.Atan2(v.X, -v.Y));
            if (angle < 0)
                angle += 360;
            return angle;
        }

        public static Vector2 GetRotated(this Vector2 v, float degrees, bool yUp = false)
        {
            if (yUp)
                degrees *= -1;
            var radians = radiansMultiplier * degrees;

            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);

            return new Vector2((float)((v.X * cos) - (v.Y * sin)), (float)((v.X * sin) + (v.Y * cos)));
        }

        public static Vector3 GetRotated(this Vector3 v, Vector3 rotation)
        {
            rotation = (float)radiansMultiplier * rotation;
            var sinAlpha = MathF.Sin(rotation.X);
            var cosAlpha = MathF.Cos(rotation.X);
            var sinBeta = MathF.Sin(rotation.Y);
            var cosBeta = MathF.Cos(rotation.Y);
            var sinGamma = MathF.Sin(rotation.Z);
            var cosGamma = MathF.Cos(rotation.Z);
            return new Vector3((cosAlpha * cosBeta) + (cosAlpha * sinBeta * sinGamma) - (sinAlpha * cosGamma) + (cosAlpha * sinBeta * cosGamma) + (sinAlpha * sinGamma),
                (sinAlpha * cosBeta) + (sinAlpha * sinBeta * sinGamma) + (cosAlpha * cosGamma) + (sinAlpha * sinBeta * cosGamma) - (cosAlpha * sinGamma),
                (-sinBeta) + (cosBeta * sinGamma) + (cosBeta * cosGamma));
        }
    }
}