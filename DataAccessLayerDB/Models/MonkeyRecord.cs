using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayerDB.Models
{
    [Table("MonkeyRecords")]
    public class MonkeyRecord
    {
        [Key]
        public int RecordId { get; set; }
        public int MonkeyId { get; set; }
        [Required]
        public string MonkeyName { get; set; }
        public int WoodId { get; set; }
        public int Seqnr { get; set; }
        public int TreeId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public MonkeyRecord()
        {
            
        }

        public MonkeyRecord( int monkeyId, string monkeyName, int woodId, int seqnr, int treeId, int x, int y)
        {
            MonkeyId = monkeyId;
            MonkeyName = monkeyName;
            WoodId = woodId;
            Seqnr = seqnr;
            TreeId = treeId;
            X = x;
            Y = y;
        }
    }
}