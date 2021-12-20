using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public static class TreeExtention
    {
        public static Tree ShortestPoint(this Tree startTree, List<Tree> trees, Forest forest)
        {
            Tree closestTree = null;
            var shortestDistance = double.MaxValue;
            var closestEdge = (new List<double>()
            {
                forest.Width*forest.Scale - startTree.Point.X,
                startTree.Point.X, 
                forest.Height*forest.Scale - startTree.Point.Y, 
                startTree.Point.Y
            }).Min();

            Parallel.ForEach(trees, (tree) =>
            {
                var distance = Math.Sqrt(Math.Pow(tree.Point.X - startTree.Point.X, 2) + Math.Pow(tree.Point.Y - startTree.Point.Y, 2));
                if (!(distance < shortestDistance)) return;
                closestTree = tree;
                shortestDistance = distance;
            });
            return shortestDistance > closestEdge ? null : closestTree;
        }
    }
}
