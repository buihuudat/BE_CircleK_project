using System.ComponentModel.DataAnnotations;

namespace CircleKAPI.Models.Producer
{
    public class Producer
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public long Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
    }
}
