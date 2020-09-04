using System;

namespace Generator
{
	public interface IRoom : ICloneable, IEquatable<IRoom>
	{
		int Width { get; }
		int Height { get; }
		Cell this[int x, int y] { get; set; }

		new IRoom Clone();
	}
}