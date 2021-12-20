using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayerDB.Models
{
    [Table("WoodRecords")]
    public class WoodRecord
    {
        [Key]
        public int RecordId { get; set; }
        public int WoodId { get; set; }
        public int TreeId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public WoodRecord()
        {
            
        }

        public WoodRecord(int woodId, int treeId, int x, int y)
        {
            WoodId = woodId;
            TreeId = treeId;
            X = x;
            Y = y;
        }
    }
}