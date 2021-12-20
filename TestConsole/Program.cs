using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayerDB;
using DataAccessLayerIO;
using DomainLayer.Interfaces;
using DomainLayer.Managers;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            //DB
            IRecordRepo repo = new RecordRepo(new RecordsDataContext(configuration));
            //IO
            IFileProcessor fileProcessor = new FileProcessor(configuration);

            var treeManager = new TreeManager();
            var monkeys = new List<Monkey>();

            var trees = treeManager.GenerateTrees(500, 500, 400, 10, 15);

            monkeys.Add(new Monkey(1, "Tom", trees[monkeys.Count], Color.Green));
            monkeys.Add(new Monkey(2, "Jerry", trees[monkeys.Count], Color.Yellow));
            monkeys.Add(new Monkey(3, "Ben", trees[monkeys.Count], Color.Red));
            monkeys.Add(new Monkey(4, "Jens", trees[monkeys.Count], Color.DarkSlateGray));

            var forest = new Forest(1, 500, 500, 15, monkeys, trees);

            var tasks = monkeys.Select(m => m.StartJumpingAsync(forest, new RecordRepo(new RecordsDataContext(configuration)))).ToList();
            tasks.Add(repo.AddWoodRecordsAsync(forest));
            await Task.WhenAll(tasks);

            var LoggingTask = new List<Task>()
            {
                fileProcessor.LogMonkeysAsync(monkeys),
                repo.AddMonkeyRecordsAsync(forest, monkeys)
            };


            //Save JPG
            fileProcessor.SaveBitmap(forest.Draw());

            await Task.WhenAll(LoggingTask);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        
    }
}
