namespace Generator
{
    public static class Cells
    {
        public static Cell Wall => new Cell('W');
        public static Cell Empty => new Cell('-');
        public static Cell Treasure => new Cell('T');
        public static Cell Trap => new Cell('X');
    }
}