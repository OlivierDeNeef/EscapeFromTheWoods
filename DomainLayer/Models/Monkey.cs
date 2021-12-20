using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Exceptions.Models;
using DomainLayer.Interfaces;


namespace DomainLayer.Models
{
    public class Monkey
    {
        public int Id { get;private set; }
        public string Name { get;private set; }
        public Tree StartTree { get;private set; }
        public Color Color { get; set; }
        private readonly List<Tree> _usedTrees;

        public Monkey(int id, string name, Tree startTree, Color color)
        {
            SetId(id);
            SetName(name);
            SetStartTree(startTree);
            Color = color;
            _usedTrees = new List<Tree>();
        }

        public void SetId(int id)
        {
            if (id < 1) throw new MonkeyException(nameof(SetId) + " - Id is kleiner dan 1");
            Id = id;
        }
        
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new MonkeyException(nameof(SetName) + " - Naam is null of leeg");
            Name = name;
        }

        public void SetStartTree(Tree tree)
        {
            StartTree = tree ?? throw new MonkeyException(nameof(SetStartTree) + " - Tree is null");
        }

        public IReadOnlyList<Tree> GetUsedTrees()
        {
            return _usedTrees;
        }


        public async Task StartJumpingAsync(Forest forest, IRecordRepo repo)
        {
            var unusedTrees = new List<Tree>(forest.GetTrees());
            var currenTree = StartTree;

            unusedTrees.Remove(currenTree);
            _usedTrees.Add(currenTree);

            while (true)
            {
                var closestTree= currenTree.ShortestPoint(unusedTrees, forest);
                if (closestTree == null) return;
                await repo.AddLog(forest, this, closestTree);
                currenTree = closestTree;
                unusedTrees.Remove(currenTree);
                _usedTrees.Add(currenTree);
            }
        }
    }
}