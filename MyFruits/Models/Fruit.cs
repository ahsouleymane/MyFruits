namespace MyFruits.Models;

public class Fruit
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual Image Image { get; set; }

    public decimal Price { get; set; }
}
