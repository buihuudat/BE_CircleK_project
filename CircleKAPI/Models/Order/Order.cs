namespace CircleKAPI.Models.Order
{
    public class Order
    {
        public int Id { get; set; }
        public string UID { get; set; } = string.Empty;
        public string Voucher { get; set; } = "";
        public string NameOfUser { get; set; } = string.Empty;
        public long Phone { get; set; }
        public string Address { get; set; } = "";

    }
}
