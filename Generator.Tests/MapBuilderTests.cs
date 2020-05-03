using System;
using NUnit.Framework;

namespace Generator.Tests
{
    [TestFixture]
    public class MapBuilderTests
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void Builder_WithSize_LessThanOne_ThrowsArgumentOutOfRangeException(int x, int y)
        {
            var builder = new Map.Builder();
            Assert.That(() => builder.WithSize(x, y), 
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
        
        [Test]
        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void Builder_WithRoomSize_LessThanOne_ThrowsArgumentOutOfRangeException(int x, int y)
        {
            var builder = new Map.Builder();
            Assert.That(() => builder.WithRoomSize(x, y), 
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}