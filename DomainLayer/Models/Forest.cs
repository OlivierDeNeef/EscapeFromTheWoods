using System.Collections.Generic;
using DomainLayer.Exceptions.Models;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace DomainLayer.Models
{
    public class Forest
    {
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public int Scale { get; set; }
        public List<Monkey> Monkeys { get; set; }
        public List<Tree> Trees { get; set; }


        public void SetMinX(int minX)
        {
            if (minX < 0) throw new ForestException(nameof(SetMinX)+" - De minimum waarde kan niet kleiner zijn 0");
            if (minX > MaxX) throw new ForestException(nameof(SetMinX) + " - De minimum waarde kan niet groter zijn de maximum waarde");
            MinX = minX;
        }
        public void SetMinY(int minY)
        {
            if (minY < 0) throw new ForestException(nameof(SetMinY) + " - De minimum waarde kan niet kleiner zijn 0");
            if (minY > MaxY) throw new ForestException(nameof(SetMinY) + " - De minimum waarde kan niet groter zijn dan de  maximum waarde");
            MinY = minY;
        }

        public void SetMaxX(int maxX)
        {
            if (maxX < 0) throw new ForestException(nameof(SetMaxX) + " - De maximum waarde kan niet kleiner zijn 0");
            if (maxX < MinX) throw new ForestException(nameof(SetMaxX) + " - De maximum waarde kan niet kleiner zijn dan de mimimum waarde");
            MaxX = maxX;
        }

        public void SetMaxY(int maxY)
        {
            if (maxY < 0) throw new ForestException(nameof(SetMaxY) + " - De maximum waarde kan niet kleiner zijn 0");
            if (maxY < MinY) throw new ForestException(nameof(SetMaxY) + " - De maximum waarde kan niet kleiner zijn dan de mimimum waarde");
            MaxY = maxY;
        }

        public Bitmap Draw()
        {
            var bm = new Bitmap((Scale*MinX), (Scale*MinY));
            using var graphics = Graphics.FromImage(bm);
            graphics.Clear(Color.White);
            foreach (var tree in Trees)
            {
                using var thickPen = new Pen(Color.Blue, 2*Scale);
                graphics.DrawEllipse(thickPen, new Rectangle(tree.Point, new Size(7*Scale, 7*Scale)));
            }
            foreach (var monkey in Monkeys)
            {
                using var brush = new SolidBrush(monkey.Color);
                graphics.FillEllipse(brush, new Rectangle(monkey.Point, new Size(7*Scale, 7*Scale)));
            }

            return bm;
        }

    }
}