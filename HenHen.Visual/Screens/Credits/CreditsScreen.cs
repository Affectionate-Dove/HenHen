// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;
using System.Collections.Generic;

namespace HenHen.Visual.Screens.Credits
{
    /// <summary>
    ///     Displays who worked on this project
    ///     and technologies used.
    /// </summary>
    public class CreditsScreen : HenHenScreen
    {
        public CreditsScreen()
        {
            var sectionsFlow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = Direction.Vertical,
                Spacing = 10,
                Padding = new() { Vertical = 10 }
            };
            AddChild(sectionsFlow);

            sectionsFlow.AddChild(GetSection("Project lead", new[] { "HoutarouOreki" }));
            sectionsFlow.AddChild(GetSection("Programmers", new[]
            {
                "CLSigma",
                "HoutarouOreki",
                "KubbaK",
                "ProgramistaZa300",
                "Lukereg"
            }));
            sectionsFlow.AddChild(GetSection("Designer", new[] { "HoutarouOreki" }));
            sectionsFlow.AddChild(GetSection("Libraries used", new[]
            {
                "raylib by raysan5 - library for videogames programming",
                "Raylib-cs by ChrisDill - C# bindings for raylib",
            }));
        }

        private static SpriteText GetSectionTitle(string sectionName) => new()
        {
            Text = sectionName,
            TextAlignment = new(0.5f, 0),
            FontSize = 32,
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y
        };

        private static SpriteText GetPersonText(string personName) => new()
        {
            Text = personName,
            TextAlignment = new(0.5f, 0),
            FontSize = 18,
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y
        };

        private static Container GetSection(string role, IEnumerable<string> names)
        {
            var container = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = Direction.Vertical,
                Spacing = 10,
                Padding = new() { Vertical = 20 }
            };

            container.AddChild(GetSectionTitle(role));

            var personsContainer = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = Direction.Vertical,
                Spacing = 4,
            };
            container.AddChild(personsContainer);
            foreach (var name in names)
                personsContainer.AddChild(GetPersonText(name));

            container.UpdateLayout(); // TODO: make this not needed // bug

            return container;
        }
    }
}