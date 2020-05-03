using System;

namespace Generator
{
    public static class MapGeneratorExtensions
    {
        public static MapGenerator AddRoomTemplates(this MapGenerator mapGenerator, params IRoom[] templates)
        {
            if (mapGenerator == null) throw new ArgumentNullException(nameof(mapGenerator));

            foreach (var template in templates)
            {
                mapGenerator.AddRoomTemplate(template);
            }

            return mapGenerator;
        }
    }
}