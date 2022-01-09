using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    public class Tree
    {
        #region Properties

        public int Id { get;private set; }
        public Point Point { get; set; }

        #endregion

        #region Constructors

        public Tree(int id, Point point)
        {
            SetId(id);
            Point = point;
           
        }

        #endregion

        #region Methodes

        public void SetId(int id)
        {
            if (id < 1) throw new TreeException(nameof(SetId) + " - Id is kleiner dan 1");
            Id = id;
        }
        public  Tree ShortestPoint(List<Tree> trees, Forest forest)
        {
            Tree closestTree = null;
            var shortestDistance = double.MaxValue;
            var closestEdge = (new List<double>()
            {
                forest.Width*forest.Scale - Point.X,
                Point.X,
                forest.Height*forest.Scale - Point.Y,
                Point.Y
            }).Min();

            Parallel.ForEach(trees, (tree) =>
            {
                var distance = Math.Sqrt(Math.Pow(tree.Point.X - Point.X, 2) + Math.Pow(tree.Point.Y - Point.Y, 2));
                if (!(distance < shortestDistance)) return;
                closestTree = tree;
                shortestDistance = distance;
            });
            return shortestDistance > closestEdge ? null : closestTree;
        }

        #endregion
    }
}