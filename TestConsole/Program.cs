using System;
using System.Collections.Generic;
using System.Drawing;
using DomainLayer.Managers;
using DomainLayer.Models;

namespace TestConsole
{
    class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        static void Main(string[] args)
        { 
           
            var treeManager = new TreeManager();
            var monkeyManager = new MonkeyManager();
            var monkeys = new List<Monkey>();

            var trees = treeManager.GenerateTrees(500, 500, 400,8, 15);
           
            monkeys.Add(new Monkey(){Color = Color.Green, Point = trees[0].Point});
            monkeys.Add(new Monkey(){Color = Color.Red, Point = trees[1].Point});
            monkeys.Add(new Monkey(){Color = Color.Gray, Point = trees[2].Point});

            var forest = new Forest() {MinX = 500, MinY = 500, Trees = trees , Monkeys = monkeys, Scale = 15};
            
            var bm = forest.Draw();

            bm.Save($"C:\\Users\\olivi\\source\\repos\\EscapeFromTheWoods\\TestConsole\\forests\\{Guid.NewGuid()}.jpg");
        }
    }
}
