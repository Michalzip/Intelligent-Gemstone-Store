using System;

namespace IntelligentStore.Domain.Models
{
    public class PreciousStone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Shine { get; set; }
        public string Origin { get; set; }

        public PreciousStone(
            int id,
            string name,
            double weight,
            string color,
            string clarity,
            decimal price,
            string type,
            string shine,
            string origin
        )
        {
            Id = id;
            Name = name;
            Weight = weight;
            Color = color;
            Clarity = clarity;
            Price = price;
            Type = type;
            Shine = shine;
            Origin = origin;
        }
    }
}
