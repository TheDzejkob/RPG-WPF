using System;

namespace RPG_WPF
{
    public class Item : IEquatable<Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Heal { get; set; }
        public int Dmg { get; set; }
        public bool Minetable { get; set; }

        public Item(int id, string name, string description, int price, int heal, int dmg, bool minetable)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Heal = heal;
            Dmg = dmg;
            Minetable = minetable;
        }

        public bool Equals(Item other)
        {
            if (other is null)
                return false;

            return Id == other.Id
                && Name == other.Name
                && Description == other.Description
                && Price == other.Price
                && Heal == other.Heal
                && Dmg == other.Dmg
                && Minetable == other.Minetable;
        }

        public override bool Equals(object obj) => Equals(obj as Item);

        public override int GetHashCode() => HashCode.Combine(Id, Name, Description, Price, Heal, Dmg, Minetable);
    }
}
