// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using System.Numerics;

namespace HenHen.Framework.Graphics3d
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Raylib_cs.Camera3D RaylibCamera { get; private set; }

        public void Update()
        {
            RaylibCamera = new Raylib_cs.Camera3D
            {
                position = Position,
                up = new Vector3(0, 1, 0),
                target = CalculatePoint(),
                fovy = 70,
                type = Raylib_cs.CameraType.CAMERA_PERSPECTIVE
            };
        }

        public Vector3 CalculatePoint()
        {
            var point = Position;
            var assistant = new Vector3(0, 0, 1).GetRotated(Rotation);
            return point += assistant;
        }
    }
}