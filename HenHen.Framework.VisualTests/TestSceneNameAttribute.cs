// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;

namespace HenHen.Framework.VisualTests
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TestSceneNameAttribute : Attribute
    {
        public string Name { get; }

        // This is a positional argument
        public TestSceneNameAttribute(string name) => Name = name;
    }
}