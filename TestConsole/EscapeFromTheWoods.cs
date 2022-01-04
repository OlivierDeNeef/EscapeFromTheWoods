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
        public static async Task StartGame( Forest forest , IConfiguration configuration)
        {
            IRecordRepo recordRepo = new RecordRepo(new RecordsDataContext(configuration));
            IFileProcessor fileProcessor = new FileProcessor(configuration);
            var tasks = forest.GetMonkeys().Select(m => m.StartJumpingAsync(forest, new RecordRepo(new RecordsDataContext(configuration)))).ToList();
            tasks.Add(recordRepo.AddWoodRecordsAsync(forest));
            await Task.WhenAll(tasks);

            var LoggingTask = new List<Task>()
            {
                fileProcessor.LogMonkeysAsync(forest.GetMonkeys().ToList()),
                recordRepo.AddMonkeyRecordsAsync(forest, forest.GetMonkeys().ToList())
            };
            //Save JPG
            fileProcessor.SaveBitmap(forest.Draw());
            await Task.WhenAll(LoggingTask);
            Thread thread = Thread.CurrentThread;
            var msg = $"forest {forest.Id} thread information\n" +
                            $"----Background: {thread.IsBackground}\n" +
                            $"----Thread Pool: {thread.IsThreadPoolThread}\n" +
                            $"----Thread ID: {thread.ManagedThreadId}\n";
            
            Console.WriteLine(msg);
        }
    }
}