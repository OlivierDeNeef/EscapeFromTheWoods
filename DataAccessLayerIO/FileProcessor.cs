using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayerIO
{

    public class FileProcessor : IFileProcessor
    {
        private readonly string _logPath;
        private readonly string _jpgPath;

        public FileProcessor(IConfiguration configuration)
        {
            var guid = Guid.NewGuid();
            _logPath = configuration.GetValue<string>("LogFilePath") + guid + ".txt";
            _jpgPath = configuration.GetValue<string>("JpgFilePath") + guid+".jpg";
        }

        public async Task LogMonkeysAsync(List<Monkey> monkeys)
        {
            var maxJumps = monkeys.Select(m => m.GetUsedTrees().Count).Max() - 1;
            await using StreamWriter file = new StreamWriter(_logPath);
            for (var i = 0; i < maxJumps; i++)
            {
                foreach (var monkey in monkeys.Where(monkey => monkey.GetUsedTrees().Count > i))
                {
                    await file.WriteLineAsync(
                        $"{monkey.Name} is in tree {monkey.GetUsedTrees()[i].Id} at ({monkey.GetUsedTrees()[i].Point.X},{monkey.GetUsedTrees()[i].Point.Y})");
                }
            }
        }

        public void SaveBitmap(Bitmap bm)
        {
            bm.Save(_jpgPath);
        }
    }
}
