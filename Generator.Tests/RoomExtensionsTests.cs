using NUnit.Framework;

namespace Generator.Tests
{
    [TestFixture]
    public class RoomExtensionsTests
    {
        private IRoom _room;

        [SetUp]
        public void SetUp()
        {
            _room = Room.OfSize(4, 4);
        }
        
        [Test]
        public void SetCell_AfterCreation_ValueUpdates()
        {
            _room.SetCell(0, 0, Cells.Wall);
            Assert.That(_room[0, 0], Is.EqualTo(Cells.Wall));
        }

        [Test]
        public void SetCell_NullRoom_ThrowsArgumentNullException()
        {
            _room = null;
            Assert.That(() => _room.SetCell(0, 0, Cells.Wall), Throws.ArgumentNullException);
        }
        
        [Test]
        public void RemoveCell_AfterCreation_DoesNothing()
        {
            _room.RemoveCell(0, 0);
            
            Assert.That(_room[0, 0], Is.EqualTo(Cells.Empty));
        }
        
        [Test]
        public void RemoveCell_AfterSet_ClearsValue()
        {
            _room.SetCell(0, 0, Cells.Wall);
            
            _room.RemoveCell(0, 0);
            
            Assert.That(_room[0, 0], Is.EqualTo(Cells.Empty));
        }

        [Test]
        public void RemoveCell_NullRoom_ThrowsArgumentNullException()
        {
            _room = null;
            Assert.That(() => _room.RemoveCell(0, 0), Throws.ArgumentNullException);
        }
    }
}