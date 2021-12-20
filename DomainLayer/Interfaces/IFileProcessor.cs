using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    public interface IFileProcessor
    {
        Task LogMonkeysAsync(List<Monkey> monkeys);
        void SaveBitmap(Bitmap bm);
    }
}