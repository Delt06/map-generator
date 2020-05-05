using System.Collections.Generic;
using Generator;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class RoomTemplates : ScriptableObject
{
        public abstract IEnumerable<IRoom> CreateRooms();
}