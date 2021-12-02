using System;
using System.Drawing;

namespace DomainLayer.Models
{
    public class Monkey
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Point Point { get; set; }
        public Color Color { get; set; }

    
    }
}