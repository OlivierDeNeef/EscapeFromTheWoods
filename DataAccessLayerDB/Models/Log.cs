using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayerDB.Models
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public int WoodId { get; set; }
        public int MonkeyId { get; set; }
        [Required]
        public string Message { get; set; }

        public Log()
        {
            
        }

        public Log(int woodId, int monkeyId, string message)
        {
            WoodId = woodId;
            MonkeyId = monkeyId;
            Message = message;
        }
    }
}