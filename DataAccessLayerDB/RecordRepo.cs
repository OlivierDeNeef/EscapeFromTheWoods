using System.Collections.Generic;
using System.Threading.Tasks;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace DataAccessLayerDB
{
    public class RecordRepo : IRecordRepo
    {
        private readonly RecordsDataContext _recordsDataContext;

        public RecordRepo(RecordsDataContext recordsDataContext)
        {
            _recordsDataContext = recordsDataContext;
        }


        private async Task SaveAndClear()
        {
            await _recordsDataContext.SaveChangesAsync();
            _recordsDataContext.ChangeTracker.Clear();
        }

        public async Task AddWoodRecordsAsync(Forest forest)
        {
            await _recordsDataContext.WoodRecords.AddRangeAsync(RecordMapper.ToWoodRecord(forest));
            await SaveAndClear();
        }
        public async Task AddMonkeyRecordsAsync(Forest forest,List<Monkey> monkeys)
        {
            foreach (var monkey in monkeys)
            {
                await _recordsDataContext.MonkeyRecords.AddRangeAsync(RecordMapper.ToMonkeyRecords(forest, monkey));
            }
            
            await SaveAndClear();
        }

        public async Task AddLogAsync(Forest forest, Monkey monkey, Tree tree)
        {
            await _recordsDataContext.AddAsync(RecordMapper.ToLog(forest, monkey, tree));
            await SaveAndClear();
        }
    }
}