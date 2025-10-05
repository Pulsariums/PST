using System;

namespace PST.Models
{
    public class CaptureRegion
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "Yeni Bölge";
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsEnabled { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public System.Drawing.Rectangle ToRectangle()
        {
            return new System.Drawing.Rectangle(X, Y, Width, Height);
        }

        public override string ToString()
        {
            return $"{Name} ({X}, {Y}, {Width}x{Height})";
        }
    }
}