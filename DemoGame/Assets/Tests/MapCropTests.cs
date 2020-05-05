using System.Linq;
using Generator;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
    [TestFixture]
    public class MapCropTests
    {
        private Map _map;

        [SetUp]
        public void SetUp()
        {
            _map = new Map.Builder()
                .WithSize(32, 32)
                .WithRoomSize(4, 4)
                .Build();
        }

        [Test]
        public void Empty()
        {
            Assert.That(() => _map.GetCroppedClone(), Throws.InvalidOperationException);
        }

        [Test]
        public void LeftBottomCorner()
        {
            _map.CreateRoomAt(0, 0);
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(1));
            Assert.That(cropped.Height, Is.EqualTo(1));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void RightTopCorner()
        {
            _map.CreateRoomAt(_map.Width - 1, _map.Height - 1);
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(1));
            Assert.That(cropped.Height, Is.EqualTo(1));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void RightBottomCorner()
        {
            _map.CreateRoomAt(_map.Width - 1, 0);
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(1));
            Assert.That(cropped.Height, Is.EqualTo(1));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void LeftTopCorner()
        {
            _map.CreateRoomAt(0, _map.Height - 1);
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(1));
            Assert.That(cropped.Height, Is.EqualTo(1));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void TwoCorners()
        {
            _map.CreateRoomAt(0, 0);
            _map.CreateRoomAt(_map.Width - 1, _map.Height - 1);
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(_map.Width));
            Assert.That(cropped.Height, Is.EqualTo(_map.Height));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void TwoArbitraryRooms()
        {
            _map.CreateRoomAt(4, 2);
            _map.CreateRoomAt(5, 4);
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(2));
            Assert.That(cropped.Height, Is.EqualTo(3));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }

        [Test]
        public void WithoutAColumn()
        {
            for (var x = 0; x < _map.Width - 1; x++)
            {
                for (var y = 0; y < _map.Height; y++)
                {
                    _map.CreateRoomAt(x, y);
                }
            }
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(_map.Width - 1));
            Assert.That(cropped.Height, Is.EqualTo(_map.Height));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void OneInCenter()
        {
            _map.CreateRoomAt(3, 4);
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(1));
            Assert.That(cropped.Height, Is.EqualTo(1));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void Full()
        {
            for (var x = 0; x < _map.Width; x++)
            {
                for (var y = 0; y < _map.Height; y++)
                {
                    _map.CreateRoomAt(x, y);
                }
            }
            
            var cropped = _map.GetCroppedClone();

            Assert.That(cropped, Is.EqualTo(_map));
        }

        [Test]
        public void NarrowRow()
        {
            for (int x = 1; x < _map.Width - 1; x++)
            {
                _map.CreateRoomAt(x, 2);
            }
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(_map.Width - 2));
            Assert.That(cropped.Height, Is.EqualTo(1));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }

        [Test]
        public void RightMiddle()
        {
            _map.CreateRoomAt(_map.Width - 1, 3);
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(1));
            Assert.That(cropped.Height, Is.EqualTo(1));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }

        [Test]
        public void Ladder()
        {
            _map.CreateRoomAt(0, 0);
            _map.CreateRoomAt(0, 1);
            _map.CreateRoomAt(1, 0);

            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(2));
            Assert.That(cropped.Height, Is.EqualTo(2));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }

        [Test]
        public void Triangle()
        {
            _map.CreateRoomAt(0, 0);
            _map.CreateRoomAt(0, 2);
            _map.CreateRoomAt(1, 1);
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(2));
            Assert.That(cropped.Height, Is.EqualTo(3));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }

        [Test]
        public void RightLadder()
        {
            _map.CreateRoomAt(_map.Width - 2, _map.Height / 2);
            _map.CreateRoomAt(_map.Width - 2, _map.Height / 2 + 1);
            _map.CreateRoomAt(_map.Width - 1, _map.Height / 2);
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(2));
            Assert.That(cropped.Height, Is.EqualTo(2));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void RightTriangle()
        {
            _map.CreateRoomAt(_map.Width - 2, 0);
            _map.CreateRoomAt(_map.Width - 2, 2);
            _map.CreateRoomAt(_map.Width - 1, 1);
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(2));
            Assert.That(cropped.Height, Is.EqualTo(3));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void Chess()
        {
            _map.CreateRoomAt(2, 0);
            _map.CreateRoomAt(2, 2);
            _map.CreateRoomAt(3, 3);
            _map.CreateRoomAt(3, 1);
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(2));
            Assert.That(cropped.Height, Is.EqualTo(4));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }

        [Test]
        public void Spider()
        {
            _map.CreateRoomAt(5, 5);
            _map.CreateRoomAt(5, 6);
            _map.CreateRoomAt(6, 6);
            _map.CreateRoomAt(6, 5);
            _map.CreateRoomAt(7, 6);
            _map.CreateRoomAt(4, 5);
            _map.CreateRoomAt(6, 7);
            _map.CreateRoomAt(5, 4);
            
            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(4));
            Assert.That(cropped.Height, Is.EqualTo(4));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
        
        [Test]
        public void SampleFromGame1()
        {
            _map = new Map.Builder()
                .WithSize(4, 4)
                .WithRoomSize(8, 8)
                .Build();

            _map.CreateRoomAt(1, 2);
            _map.CreateRoomAt(2, 2);
            _map.CreateRoomAt(3, 2);

            var cropped = _map.GetCroppedClone();
            
            Assert.That(cropped.Width, Is.EqualTo(3));
            Assert.That(cropped.Height, Is.EqualTo(1));
            Assert.That(cropped.ToArray(), Is.EqualTo(_map.ToArray()));
        }
    }
}