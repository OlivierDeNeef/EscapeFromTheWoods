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
        public int Id { get;private set; }
        public Point Point { get; set; }

        public Tree(int id, Point point)
        {
            SetId(id);
            Point = point;
           
        }
        public void SetId(int id)
        {
            if (id < 1) throw new TreeException(nameof(SetId) + " - Id is kleiner dan 1");
            Id = id;
        }
    }
}