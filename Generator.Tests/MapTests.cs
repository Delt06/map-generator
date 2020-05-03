using System;
using System.Linq;
using NUnit.Framework;

namespace Generator.Tests
{
    [TestFixture]
    public class MapTests
    {
        private Map _map;

        [SetUp]
        public void SetUp()
        {
            _map = new Map.Builder()
                .WithRoomSize(4, 4)
                .WithSize(4, 4)
                .Build();
        }

        [Test]
        public void HasRoomAt_AfterCreated_ReturnsFalseForAll()
        {
            var falseForAll = Enumerable2D
                .Range(_map.Width, _map.Height)
                .All(p => !_map.HasRoomAt(p.x, p.y));
            
            Assert.That(falseForAll);
        }

        [Test]
        public void HasRoomAt_WhenCreated_ReturnsTrueForThat()
        {
            _map.CreateRoomAt(2, 2);
            
            Assert.That(_map.HasRoomAt(2, 2));
            Assert.That(!_map.HasRoomAt(0, 1));
            Assert.That(!_map.HasRoomAt(1, 1));
        }
        
        [Test]
        public void HasRoomAt_WhenCreatedAndThenRemoved_ReturnsFalse()
        {
            _map.CreateRoomAt(2, 2);
            _map.RemoveRoomAt(2, 2);
            
            Assert.That(!_map.HasRoomAt(2, 2));
            Assert.That(!_map.HasRoomAt(0, 1));
            Assert.That(!_map.HasRoomAt(1, 1));
        }

        [Test]
        public void This_WhenAccessExistingRoom_ReturnsThatRoom()
        {
            var createdRoom = _map.CreateRoomAt(2, 2);
            
            Assert.That(_map[2, 2], Is.EqualTo(createdRoom));
        }

        [Test]
        public void This_WhenAccessNonExistingRoom_ThrowsArgumentException()
        {
            Assert.That(() => _map[2, 2], Throws.ArgumentException);
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(-1, -1)]
        [TestCase(0, 4)]
        [TestCase(4, 0)]
        [TestCase(4, 4)]
        public void This_InvalidArguments_ThrowsArgumentOutOfRangeException(int x, int y)
        {
            Assert.That(() => _map[x, y], Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 2, 2, 2)]
        [TestCase(1, 2, 1, 2)]
        public void TotalSize_ReturnsRoomSizeTimesRoomCount(int width, int height, int roomWidth, int roomHeight)
        {
            var map = new Map.Builder()
                .WithSize(width, height)
                .WithRoomSize(roomWidth, roomHeight)
                .Build();
            
            Assert.That(map.TotalWidth, Is.EqualTo(width * roomWidth));
            Assert.That(map.TotalHeight, Is.EqualTo(height * roomHeight));
        }

        [Test]
        public void GetEnumerator_ReturnsAllExistingRooms()
        {
            var room1 = _map.CreateRoomAt(0, 0);
            var room2 = _map.CreateRoomAt(2, 2);
            var room3 = _map.CreateRoomAt(1, 1);
            
            Assert.That(_map, Is.EqualTo(new []{room1, room2, room3}));
        }

        [Test]
        public void ToString_WhenEmpty_HeightIsValid()
        {
            var mapAsString = _map.ToString();
            var lines = mapAsString.Trim('\n').Split('\n');
            
            Assert.That(lines.Length, Is.EqualTo(_map.TotalHeight));
        }
        
        [Test]
        public void ToString_WhenEmpty_WidthIsValid()
        {
            var mapAsString = _map.ToString();
            var lines = mapAsString.Trim('\n').Split('\n');
            
            Assert.That(lines.All(l => l.Length == _map.TotalWidth));
        }
        
        [Test]
        public void ToString_WhenNotEmpty_HeightIsValid()
        {
            _map.CreateRoomAt(0, 0);
            _map.CreateRoomAt(2, 3);
            
            var mapAsString = _map.ToString();
            var lines = mapAsString.Trim('\n').Split('\n');
            
            Assert.That(lines.Length, Is.EqualTo(_map.TotalHeight));
        }
        
        [Test]
        public void ToString_WhenNotEmpty_WidthIsValid()
        {
            _map.CreateRoomAt(0, 0);
            _map.CreateRoomAt(2, 3);

            var mapAsString = _map.ToString();
            var lines = mapAsString.Trim('\n').Split('\n');
            
            Assert.That(lines.All(l => l.Length == _map.TotalWidth));
        }

        [Test]
        public void Clear_HavingSomeRooms_RemovesALl()
        {
            _map.CreateRoomAt(0, 0);
            _map.CreateRoomAt(2, 3);

            _map.Clear();
            
            Assert.That(_map.Count(), Is.EqualTo(0));
        }
    }
}