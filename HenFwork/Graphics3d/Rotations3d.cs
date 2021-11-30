// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Numerics;

namespace HenFwork.Graphics3d
{
    public static class Rotations3d
    {
        public static float DegreesToRadians(float angle) => MathF.PI * angle / 180;

        public static float RadiansToDegrees(float angle) => 180 * angle / MathF.PI;

        /// <summary>
        ///     Converts euler angles to axis and rotation.
        /// </summary>
        /// <remarks>
        /// Credit: <see href="http://www.euclideanspace.com/maths/geometry/rotations/conversions/eulerToAngle/"/>.
        /// </remarks>
        public static (Vector3 axis, float angle) EulerToAxisAngle(Vector3 eulerRotation)
        {
            var heading = DegreesToRadians(eulerRotation.Z);
            var attitude = DegreesToRadians(eulerRotation.X);
            var bank = DegreesToRadians(eulerRotation.Y);

            var c1 = MathF.Cos(heading / 2);
            var s1 = MathF.Sin(heading / 2);
            var c2 = MathF.Cos(attitude / 2);
            var s2 = MathF.Sin(attitude / 2);
            var c3 = MathF.Cos(bank / 2);
            var s3 = MathF.Sin(bank / 2);
            var c1c2 = c1 * c2;
            var s1s2 = s1 * s2;
            var w = (c1c2 * c3) - (s1s2 * s3);
            var x = (c1c2 * s3) + (s1s2 * c3);
            var y = (s1 * c2 * c3) + (c1 * s2 * s3);
            var z = (c1 * s2 * c3) - (s1 * c2 * s3);
            var angleRadians = 2 * MathF.Acos(w);
            var angleDegrees = RadiansToDegrees(angleRadians);
            var norm = (x * x) + (y * y) + (z * z);

            if (norm == 0)
            {
                // when all euler angles are zero angle =0 so
                // we can set axis to anything to avoid divide by zero
                x = 1;
                y = z = 0;
            }
            else
            {
                norm = MathF.Sqrt(norm);
                x /= norm;
                y /= norm;
                z /= norm;
            }
            return (new(-z, x, -y), angleDegrees);
        }
    }
}