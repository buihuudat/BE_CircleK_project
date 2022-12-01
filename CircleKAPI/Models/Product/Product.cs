using System.ComponentModel.DataAnnotations;

namespace CircleKAPI.Models.Product
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProducerId { get; set; }
    public int Count { get; set; } = 0;
    public string Image { get; set; } = "";
    public string Type { get; set; } = "";
    public int Price { get; set; } = 0;
    public DateTime HXS { get; set; }
    public DateTime HSD { get; set; }
  }
}
