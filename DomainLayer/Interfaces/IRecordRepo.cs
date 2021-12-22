using System.Collections.Generic;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    public interface IRecordRepo
    {
        Task AddWoodRecordsAsync(Forest forest);
        Task AddMonkeyRecordsAsync(Forest forest,List<Monkey> monkeys);
        Task AddLogAsync(Forest forest, Monkey monkey, Tree tree);
    }
}