using System.Linq;
using NUnit.Framework;

namespace Generator.Tests
{
    [TestFixture]
    public class MapExtensionsTests
    {
        private Map _map;

        [SetUp]
        public void SetUp()
        {
            _map = new Map.Builder()
                .WithSize(4, 4)
                .WithRoomSize(4, 4)
                .Build();
        }

        [Test]
        public void AddOuterWalls_WhenOneRoom_SurroundedByWalls()
        {
            var room = _map.CreateRoomAt(0, 0);
            _map.AddOuterWalls();

            Assert.That(Enumerable2D.Range(room.Width, room.Height)
                .All(p => !(p.x == 0 || p.y == 0 || p.x == room.Width - 1 || p.y == room.Height - 1) ||
                          room[p.x, p.y] == Cells.Wall));
        }

        [Test]
        public void AddOuterWalls_WhenMapIsNull_ThrowsArgumentNullException()
        {
            _map = null;
            
            Assert.That(() => _map.AddOuterWalls(), Throws.ArgumentNullException);
        }

        [Test]
        public void TryGetRoomAt_WhenMapIsNull_ThrowsArgumentNullException()
        {
            _map = null;
            
            Assert.That(() => _map.TryGetRoomAt(0, 0, out _), Throws.ArgumentNullException);
        }
        
        [Test]
        public void TryGetRoomAt_WhenItExists_ReturnTrue()
        {
            var room = _map.CreateRoomAt(0, 0);
            var found = _map.TryGetRoomAt(0, 0, out var foundRoom);
            
            Assert.That(found);
            Assert.That(room, Is.EqualTo(foundRoom));
        }
        
        [Test]
        public void TryGetRoomAt_WhenDoesNotExist_ReturnFalse()
        {
            var found = _map.TryGetRoomAt(0, 0, out _);
            
            Assert.That(!found);
        }
    }
}