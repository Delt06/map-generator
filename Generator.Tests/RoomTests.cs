using System;
using System.Linq;
using NUnit.Framework;

namespace Generator.Tests
{
    [TestFixture]
    public class RoomTests
    {
        private IRoom _room;

        [SetUp]
        public void SetUp()
        {
            _room = Room.OfSize(4, 4);
        }

        [Test]
        public void Clone_ReturnedObjectIsEqual()
        {
            _room.SetCell(0, 0, Cells.Wall)
                .SetCell(2, 3, Cells.Wall);    
            
            var clone = _room.Clone();
            
            Assert.That(clone, Is.EqualTo(_room));
        }

        [Test]
        public void Equals_ToSimilarRoom_ReturnsTrue()
        {
            _room.SetCell(1, 1, Cells.Wall);

            var otherRoom = Room.OfSize(_room.Width, _room.Height)
                .SetCell(1, 1, Cells.Wall);
            
            Assert.That(_room, Is.EqualTo(otherRoom));
        }
        
        [Test]
        public void Equals_ToDifferentRoom_ReturnsFalse()
        {
            _room.SetCell(1, 1, Cells.Wall);

            var otherRoom = Room.OfSize(_room.Width, _room.Height)
                .SetCell(1, 2, Cells.Wall);
            
            Assert.That(_room, Is.Not.EqualTo(otherRoom));
        }
        
        [Test]
        public void Equals_ToItself_ReturnsTrue()
        {
            _room.SetCell(1, 1, Cells.Wall);
            
            Assert.That(_room, Is.EqualTo(_room));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void Ctor_WhenSizeIsLessThanOne_ThrowsArgumentOutOfRangeException(int x, int y)
        {
            Assert.That(() => Room.OfSize(x, y), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Equals_WhenOtherIsNull_ReturnsFalse()
        {
            Assert.That(_room.Equals(null), Is.False);
        }
        
        [Test]
        [TestCase(4, 5)]
        [TestCase(4, 3)]
        [TestCase(5, 4)]
        [TestCase(3, 4)]
        [TestCase(3, 3)]
        [TestCase(5, 5)]
        public void Equals_WhenSizesAreDifferent_ReturnsFalse(int x, int y)
        {
            var otherRoom = Room.OfSize(x, y);
            
            Assert.That(_room.Equals(otherRoom), Is.False);
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(4, 0)]
        [TestCase(0, 4)]
        [TestCase(4, 4)]
        public void ThisGet_WhenIndexIsInvalid_ThrowsArgumentOutOfRangeException(int x, int y)
        {
            Assert.That(() => _room[x, y], Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
        
        [Test]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(4, 0)]
        [TestCase(0, 4)]
        [TestCase(4, 4)]
        public void ThisSet_WhenIndexIsInvalid_ThrowsArgumentOutOfRangeException(int x, int y)
        {
            Assert.That(() => _room[x, y] = Cells.Empty, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Equals_ToNull_ReturnsFalse()
        {
            Assert.That(((object)_room).Equals(null), Is.False);
        }
        
        [Test]
        public void Equals_ToOtherDifferentRoom_ReturnsFalse()
        {
            var otherRoom = Room.OfSize(4, 4)
                .SetCell(0, 0, Cells.Wall);
            
            Assert.That(((object)_room).Equals(otherRoom), Is.False);
        }
        
        [Test]
        public void Equals_ToOtherButSimilarRoom_ReturnsTrue()
        {
            var otherRoom = Room.OfSize(4, 4);
            
            Assert.That(((object)_room).Equals(otherRoom));
        }

        [Test]
        public void GetHashCode_WhenSimilarRooms_CodesAreEqual()
        {
            var room = Room.OfSize(4, 4);
            
            Assert.That(_room.GetHashCode(), Is.EqualTo(room.GetHashCode()));
        }

        [Test]
        public void ToString_SizeIsCorrect()
        {
            var roomAsString = _room.ToString();
            var lines = roomAsString.Trim('\n').Split('\n');
            
            Assert.That(lines.Length, Is.EqualTo(_room.Height));
            Assert.That(lines.All(l => l.Length == _room.Width));
        }

        [Test]
        public void SystemICloneableClone_ReturnsEqualObject()
        {
            ICloneable roomAsCloneable = _room;
            
            Assert.That(roomAsCloneable.Clone(), Is.EqualTo(_room));
        }
    }
}