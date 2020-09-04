using System.Linq;
using NUnit.Framework;

namespace Generator.Tests
{
	[TestFixture]
	public class MapGeneratorTests
	{
		private Map _map;
		private MapGenerator _mapGenerator;

		[SetUp]
		public void SetUp()
		{
			_map = new Map.Builder()
				.WithSize(4, 4)
				.WithRoomSize(4, 4)
				.Build();

			_mapGenerator = new MapGenerator(_map);
		}

		[Test]
		public void AddRoomTemplate_WhenCorrect_TemplateGetsAdded()
		{
			var room = Room.OfSize(_map.Width, _map.Height);

			_mapGenerator.AddRoomTemplate(room);

			Assert.That(_mapGenerator.RoomTemplates, Is.EqualTo(new[] {room}));
		}

		[Test]
		public void AddRoomTemplate_WhenNull_ThrowsArgumentNullException()
		{
			Assert.That(() => _mapGenerator.AddRoomTemplate(null), Throws.ArgumentNullException);
		}

		[Test]
		public void AddRoomTemplate_OfDifferentSize_ThrowsArgumentException()
		{
			var room = Room.OfSize(2, 2);

			Assert.That(() => _mapGenerator.AddRoomTemplate(room), Throws.ArgumentException);
		}

		[Test]
		public void RemoveRoomTemplate_WhenDidNotExist_NoEffect()
		{
			_mapGenerator.RemoveRoomTemplate(Room.OfSize(4, 4));
			Assert.That(_mapGenerator.RoomTemplates.Count, Is.EqualTo(0));
		}

		[Test]
		public void RemoveRoomTemplate_WhenExisted_RemovesFirstOccurence()
		{
			var room = Room.OfSize(_map.Width, _map.Height);
			_mapGenerator.AddRoomTemplate(room);
			_mapGenerator.AddRoomTemplate(room);

			_mapGenerator.RemoveRoomTemplate(room);

			Assert.That(_mapGenerator.RoomTemplates, Is.EqualTo(new[] {room}));
		}

		[Test]
		public void Generate_WhenNoTemplates_MapBecomesEmpty()
		{
			_map.CreateRoomAt(0, 0);
			_map.CreateRoomAt(2, 3);

			_mapGenerator.Generate();

			Assert.That(_map.Count(), Is.EqualTo(0));
		}

		[Test]
		public void Generate_WhenHasTemplates_MapIsNotEmpty()
		{
			var room = Room.OfSize(_map.RoomWidth, _map.RoomHeight);
			_mapGenerator.AddRoomTemplate(room);

			_mapGenerator.Generate();

			Assert.That(_map.Count(), Is.GreaterThan(0));
		}
	}
}