using System;
using System.Collections.Generic;
using DomainLayer.Exceptions.Models;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace DomainLayer.Models
{
    public class Forest
    {
        #region Fields

        private List<Monkey> _monkeys;
        private List<Tree> _trees;

        #endregion

        #region Properties

        public int Id { get;private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Scale { get; private set; }

        #endregion

        #region Construtors

        public Forest(int id, int width, int height, int scale, List<Monkey> monkeys, List<Tree> trees)
        {
            SetHeight(height);
            SetWidth(width);
            SetScale(scale);
            SetId(id);
            SetMonkeys(monkeys);
            SetTrees(trees);
        }

        #endregion

        #region Methodes

        public void SetId(int id)
        {
            if (id < 1) throw new ForestException(nameof(SetId) + " - Id is kleiner dan 1");
            Id = id;
        }
        public void SetWidth(int width)
        {
            if (width < 0) throw new ForestException(nameof(SetWidth)+" - De minimum waarde kan niet kleiner zijn 0");
            Width = width;
        }
        public void SetHeight(int height)
        {
            if (height < 0) throw new ForestException(nameof(SetHeight) + " - De minimum waarde kan niet kleiner zijn 0");
            Height = height;
        }
        public void SetScale(int scale)
        {
            if (scale < 0) throw new ForestException(nameof(SetScale) + " - De minimum waarde kan niet kleiner zijn 0");
            Scale = scale;
        }
        public void SetTrees(List<Tree> trees)
        {
            if (trees == null || trees.Count < 1) throw new ForestException(nameof(SetTrees) + " - Bevat geen trees");
            _trees = trees;
        }
        public IReadOnlyList<Tree> GetTrees()
        {
            return _trees;
        }
        public void SetMonkeys(List<Monkey> monkeys)
        {
            if (monkeys == null || monkeys.Count < 1) throw new ForestException(nameof(SetMonkeys) + " - Bevat geen monkeys");
            _monkeys = monkeys;
        }
        public IReadOnlyList<Monkey> GetMonkeys()
        {
            return _monkeys;
        }
        public Bitmap Draw()
        {
            var bm = new Bitmap((Scale*Width), (Scale*Height));
            using var graphics = Graphics.FromImage(bm);
            graphics.Clear(Color.Black);
            foreach (var tree in _trees)
            {
                using var thickPen = new Pen(Color.Blue, 2 * Scale);
                var centerPoint = new Point(tree.Point.X - 5 * Scale, tree.Point.Y - 5 * Scale);
                graphics.DrawEllipse(thickPen, new Rectangle( centerPoint,new Size(10*Scale,10*Scale)));
            }
            foreach (var monkey in _monkeys)
            {
                using var thickPen = new Pen(monkey.Color, 2 * Scale);
                if (monkey.GetUsedTrees().Count > 1)
                {
                    graphics.DrawLines(thickPen, monkey.GetUsedTrees().Select(tr=>tr.Point).ToArray());
                }
                using var brush = new SolidBrush(monkey.Color);
                var centerPoint = new Point(monkey.StartTree.Point.X - 5 * Scale, monkey.StartTree.Point.Y - 5 * Scale);
                graphics.FillEllipse(brush, new Rectangle(centerPoint, new Size(10 * Scale, 10* Scale)));
            }
            return bm;
        }

        #endregion

    }
}