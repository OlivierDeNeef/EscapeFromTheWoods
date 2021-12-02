using System;
using System.Collections.Generic;
using System.Drawing;
using DomainLayer.Models;

namespace DomainLayer.Managers
{
    public class TreeManager
    {
        public List<Tree> GenerateTrees(int maxX, int maxY, int amount, int offset, int scale)
        {
            var rnd = new Random();
            var listOfTrees = new List<Tree>();
            for (var i = 0; i < amount; i++)
            {
                var id = Guid.NewGuid();
                var point = new Point(rnd.Next(offset/10, (maxX-offset)/10)*scale*10, rnd.Next(offset/10, (maxY-offset)/10)*scale*10);
                listOfTrees.Add(new Tree(){Id = id,Point = point});
                
            }

            return listOfTrees;
        }
    }
}