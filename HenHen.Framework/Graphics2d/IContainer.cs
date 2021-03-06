﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d.Layouts;
using System.Collections.Generic;

namespace HenHen.Framework.Graphics2d
{
    public interface IContainer
    {
        ContainerLayoutInfo ContainerLayoutInfo { get; }
    }

    public interface IContainer<T> : IContainer
    {
        public IEnumerable<T> Children { get; }
    }
}