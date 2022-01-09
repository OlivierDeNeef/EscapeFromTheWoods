using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Managers;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;

namespace TestConsole
{
     class  Program
    {
        public static IServiceProvider ServiceProvider { get;private set; }

        static async Task Main(string[] args)
        {
            //Config
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Forest calculating... \n");

            var treeManager = new TreeManager();
            var forests = new List<Forest>();
           

            //Voegt i aantal keer een bos toe met random bomen en appen
            for (int i = 1; i <= 20; i++)
            {   
                var monkeys = new List<Monkey>();
                var trees = treeManager.GenerateTrees(500, 500, 500, 10, 15);
                monkeys.Add(new Monkey(1, "Tom", trees[monkeys.Count], Color.Green));
                monkeys.Add(new Monkey(2, "Jerry", trees[monkeys.Count], Color.Yellow));
                monkeys.Add(new Monkey(3, "Ben", trees[monkeys.Count], Color.Red));
                monkeys.Add(new Monkey(4, "Jens", trees[monkeys.Count], Color.DarkSlateGray));
                forests.Add(new Forest(i, 500, 500, 15, monkeys, trees));
            }

            //Laat alle apen voor alle bossen ontsnappen. geeft een lijst van tasks terug.
            var games = forests.Select(f=> EscapeFromTheWoods.StartGameAsync(f, configuration)).ToList();

            //Wacht tot alle tasks van lijst gedaan zijn.
            await Task.WhenAll(games);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds +" ms");
        }

        
    }
}
