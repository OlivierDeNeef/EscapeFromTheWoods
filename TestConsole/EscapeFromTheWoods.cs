using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLayerDB;
using DataAccessLayerIO;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;

namespace TestConsole
{
    public static class EscapeFromTheWoods
    {
        public static async Task StartGameAsync( Forest forest , IConfiguration configuration)
        {
            IRecordRepo recordRepo = new RecordRepo(new RecordsDataContext(configuration));
            IFileProcessor fileProcessor = new FileProcessor(configuration);

            // Laat alle apen die in het bos zitten ontsnappen en dit geeft een lijst terug van tasks.
            var tasks = forest.GetMonkeys().Select(m => m.StartJumpingAsync(forest, new RecordRepo(new RecordsDataContext(configuration)))).ToList();

            //Voegt een forest logging task toe aan de lijst
            tasks.Add(recordRepo.AddWoodRecordsAsync(forest));

            //Wacht tot alle tasks gedaan zijn.
            await Task.WhenAll(tasks);
            
            //Voegt monkey file logging task toe en monkey db logging task toe.
            var loggingTasks = new List<Task>()
            {
                fileProcessor.LogMonkeysAsync(forest.GetMonkeys().ToList()),
                recordRepo.AddMonkeyRecordsAsync(forest, forest.GetMonkeys().ToList())
            };
            //Save JPG
            fileProcessor.SaveBitmap(forest.Draw());

            //Wacht tot als de logging tasks gedaan zijn.
            await Task.WhenAll(loggingTasks);

            
            Console.WriteLine($"forest {forest.Id} thread information\n" +
                              $"----Thread ID: {Thread.CurrentThread.ManagedThreadId}\n");
        }
    }
}