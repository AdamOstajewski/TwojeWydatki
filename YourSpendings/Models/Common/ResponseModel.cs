using System.ComponentModel.DataAnnotations.Schema;

namespace YourSpendings.Models.Common
{
    public class ResponseModel
    {
        [NotMapped]
        public string? Message { get; set; }
        [NotMapped]
        public bool? IsSuccess { get; set; }
        [NotMapped]
        public bool? IsResponse { get; set; }
    }
}
