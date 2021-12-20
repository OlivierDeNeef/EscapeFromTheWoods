using System.Collections.Generic;
using System.Linq;
using DataAccessLayerDB.Models;
using DomainLayer.Models;

namespace DataAccessLayerDB
{
    public static class RecordMapper
    {
        public static List<WoodRecord>  ToWoodRecord(Forest forest)
        {
            return forest.GetTrees().Select(t => new WoodRecord(forest.Id, t.Id, t.Point.X, t.Point.Y)).ToList();
        }

        public static List<MonkeyRecord> ToMonkeyRecords(Forest forest, Monkey monkey)
        {
            return monkey.GetUsedTrees().Select((tree, index) =>
                    new MonkeyRecord(monkey.Id, monkey.Name, forest.Id, index, tree.Id, tree.Point.X, tree.Point.Y))
                .ToList();
        }

        public static Log ToLog(Forest forest, Monkey monkey, Tree tree)
        {
            return new Log(forest.Id, monkey.Id, $"{monkey.Name} is now in tree {tree.Id} at loction ({tree.Point.X},{tree.Point.Y})");
        }
    }
}