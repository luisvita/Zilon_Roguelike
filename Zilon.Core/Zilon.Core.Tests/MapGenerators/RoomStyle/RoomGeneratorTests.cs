﻿using System;
using System.Collections.Generic;

using FluentAssertions;

using NUnit.Framework;

using Zilon.Core.Tactics.Spatial;

namespace Zilon.Core.MapGenerators.RoomStyle.Tests
{
    [TestFixture]
    [Category("Integration")]
    public class RoomGeneratorTests
    {
        [Test]
        public void GenerateRoomsInGrid_WithFixCompact_NotThrowsExceptions()
        {
            // ARRANGE
            var random = new FixCompactRoomGeneratorRandomSource();
            var settings = new RoomGeneratorSettings();
            var generator = new RoomGenerator(random);
            var graphMap = new GraphMap();


            // ACT
            Action act = () =>
            {
                var rooms = generator.GenerateRoomsInGrid();
                var edgeHash = new HashSet<string>();
                generator.CreateRoomNodes(graphMap, rooms, edgeHash);
                generator.BuildRoomCorridors(graphMap, rooms, edgeHash);
            };



            // ASSERT
            act.Should().NotThrow();
        }


        [Test]
        public void GenerateRoomsInGrid_WithFixLarge_NotThrowsExceptions()
        {
            // ARRANGE
            var random = new FixLargeRoomGeneratorRandomSource();
            var generator = new RoomGenerator(random);
            var graphMap = new GraphMap();


            // ACT
            Action act = () =>
            {
                var rooms = generator.GenerateRoomsInGrid();
                var edgeHash = new HashSet<string>();
                generator.CreateRoomNodes(graphMap, rooms, edgeHash);
                generator.BuildRoomCorridors(graphMap, rooms, edgeHash);
            };



            // ASSERT
            act.Should().NotThrow();
        }
    }
}