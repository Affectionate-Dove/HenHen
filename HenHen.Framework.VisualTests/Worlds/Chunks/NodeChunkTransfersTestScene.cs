// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Graphics2d;
using HenHen.Framework.Numerics;
using HenHen.Framework.UI;
using HenHen.Framework.VisualTests.Input;
using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Chunks.Simulation;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Numerics;

namespace HenHen.Framework.VisualTests.Worlds.Chunks
{
    public class NodeChunkTransfersTestScene : VisualTestScene
    {
        private readonly ChunksManager chunksManager;
        private readonly ChunksSimulationManager simulationManager;
        private readonly TestNode[] nodes;
        private double time;

        public NodeChunkTransfersTestScene()
        {
            const float chunkSize = 40;
            const int chunksAmount = 10;
            chunksManager = new ChunksManager(new Vector2(chunksAmount), chunkSize);
            simulationManager = new ChunksSimulationManager(chunksManager);
            CreateChunkDrawables();
            nodes = new TestNode[3];
            nodes[0] = new TestNode(50);
            nodes[1] = new TestNode(30);
            nodes[2] = new TestNode(10);
            var i = 0;
            const float center = chunkSize * chunksAmount * 0.5f;
            foreach (var node in nodes)
            {
                chunksManager.AddNode(node);
                AddChild(new TestNodeDisplay(node, i));
                node.Position = new Vector3(0, 0, 0);
                i++;
            }
            AddChild(new SpriteText
            {
                RelativeSizeAxes = Axes.Both,
                Text = "Each circle represents a node of a different size.\n" +
                "Chunks - children count legend:\n" +
                "red - 3\n" +
                "yellow - 2\n" +
                "green - 1\n" +
                "gray - 0\n"
            });
        }

        public override bool OnActionPressed(SceneControls action)
        {
            var returnValue = false;
            foreach (var node in nodes)
            {
                switch (action)
                {
                    case SceneControls.Left:
                        node.Velocity.X -= 1;
                        returnValue = true;
                        break;

                    case SceneControls.Right:
                        node.Velocity.X += 1;
                        returnValue = true;
                        break;

                    case SceneControls.Up:
                        node.Velocity.Y += 1;
                        returnValue = true;
                        break;

                    case SceneControls.Down:
                        node.Velocity.Y -= 1;
                        returnValue = true;
                        break;
                }
            }
            return returnValue;
        }

        public override void OnActionReleased(SceneControls action)
        {
            foreach (var node in nodes)
            {
                switch (action)
                {
                    case SceneControls.Left:
                        node.Velocity.X += 1;
                        break;

                    case SceneControls.Right:
                        node.Velocity.X -= 1;
                        break;

                    case SceneControls.Up:
                        node.Velocity.Y -= 1;
                        break;

                    case SceneControls.Down:
                        node.Velocity.Y += 1;
                        break;
                }
            }
        }

        protected override void OnUpdate()
        {
            time++;
            simulationManager.Simulate(time);
            base.OnUpdate();
        }

        private void CreateChunkDrawables()
        {
            foreach (var chunk in chunksManager.Chunks.Values)
                AddChild(new ChunkDrawable(chunk));
        }

        private class TestNodeDisplay : Framework.Graphics2d.Circle
        {
            private readonly TestNode node;

            public TestNodeDisplay(TestNode node, int i)
            {
                Size = node.CollisionBody.BoundingBox.ToTopDownRectangle().Size;
                Anchor = new Vector2(0, 1);
                Origin = new Vector2(0.5f, 0.5f);
                this.node = node;
                Color = i switch
                {
                    0 => new ColorInfo(0, 50, 200, 100),
                    1 => new ColorInfo(200, 200, 0, 70),
                    2 => new ColorInfo(255, 0, 100, 50),
                    _ => new ColorInfo(0, 0, 0, 20),
                };
            }

            protected override void OnUpdate()
            {
                base.OnUpdate();
                Offset = node.Position.ToTopDownPoint() * new Vector2(1, -1);
                Console.WriteLine(node.Position);
            }
        }

        private class TestNode : Node
        {
            public Vector2 Velocity;

            public TestNode(float radius)
            {
                CollisionBody = new Collisions.CollisionBody(new Sphere[]
                {
                    new() { Radius = radius }
                });
            }

            protected override void Simulation(double duration)
            {
                base.Simulation(duration);
                if (Velocity.LengthSquared() == 0)
                    return;
                var velocity = Vector3.Normalize(new Vector3(Velocity.X, 0, Velocity.Y));
                const float speed = 1;
                Position += velocity * (float)duration * speed;
            }
        }

        private class ChunkDrawable : Rectangle
        {
            private readonly Chunk chunk;

            public ChunkDrawable(Chunk chunk)
            {
                this.chunk = chunk;
                var coordinates = chunk.Coordinates;
                coordinates = new(coordinates.Left, coordinates.Right, -coordinates.Top, -coordinates.Bottom);
                Anchor = new Vector2(0, 1);
                Origin = new Vector2(0, 1);
                Offset = new Vector2(coordinates.Left, coordinates.Top);
                Size = chunk.Coordinates.Size;
            }

            protected override void OnUpdate()
            {
                base.OnUpdate();
                Color = chunk.Nodes.Count switch
                {
                    3 => new ColorInfo(120, 0, 0),
                    2 => new ColorInfo(80, 80, 0),
                    1 => new ColorInfo(0, 80, 0),
                    0 => new ColorInfo(20, 20, 20),
                    _ => new ColorInfo(40, 40, 40)
                };
            }
        }
    }
}