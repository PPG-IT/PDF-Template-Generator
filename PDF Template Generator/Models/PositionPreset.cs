namespace PDF_Template_Generator.Models
{
    public class PositionPreset
    {
        public string Name { get; set; } = string.Empty;
        public int X { get; set; }
        public int Y { get; set; }
        public float Scale { get; set; } = 100f;
        public int Rotation { get; set; } = 0; // 0, 90, 180, 270 degrees
        public ProductLine ProductLine { get; set; }
        public string ElementType { get; set; } = string.Empty; // "Barcode", "Logo", "Text"

        public PositionPreset() { }

        public PositionPreset(string name, int x, int y, ProductLine productLine, string elementType, float scale = 100f, int rotation = 0)
        {
            Name = name;
            X = x;
            Y = y;
            Scale = scale;
            Rotation = rotation;
            ProductLine = productLine;
            ElementType = elementType;
        }

        public override string ToString() => Name;
    }
} 