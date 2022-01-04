using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace DomainLayer.Managers
{
    public static class EscapeFromTheWoodsManager
    {
        public static async Task StartGame( Forest forest , IRecordRepo recordRepo, IFileProcessor fileProcessor)
        {
            var tasks = forest.GetMonkeys().Select(m => m.StartJumpingAsync(forest, recordRepo)).ToList();
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
        }
    }
}