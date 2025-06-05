using System.Collections.Generic;
using System.Linq;
public class Rarity {
    public string RarityName { get; set; }
    public float RarityValue { get; set; }

    public Rarity(string rarityName, float rarityValue) {
        RarityName = rarityName;
        RarityValue = rarityValue;
    }

    public static readonly Rarity Common = new("Common", 30f);
    public static readonly Rarity Uncommon = new("Uncommon", 28f);
    public static readonly Rarity Rare = new("Rare", 25f);
    public static readonly Rarity Epic = new("Epic", 10f);
    public static readonly Rarity Legendary = new("Legendary", 5f);
    public static readonly Rarity Mythic = new("Mythic", 2f);
    private static readonly List<Rarity> _values = new() {
        Common, Uncommon, Rare, Epic, Legendary, Mythic
    };
    public static IEnumerable<Rarity> Values => _values;
}

public class RarityExtensions {
    private readonly Dictionary<float, Rarity> _byValue;
    private readonly Dictionary<string, Rarity> _byName;

    public static RarityExtensions Instance { get; } = new RarityExtensions();

    public RarityExtensions() {
        _byValue = Rarity.Values.ToDictionary(r => r.RarityValue);
        _byName = Rarity.Values.ToDictionary(r => r.RarityName, System.StringComparer.OrdinalIgnoreCase);
    }

    public Rarity this[float value] =>
        _byValue.TryGetValue(value, out var rarity) ? rarity : null;

    public Rarity this[string name] =>
        _byName.TryGetValue(name, out var rarity) ? rarity : null;

    public Rarity GetByName(string name) => this[name];
    public Rarity GetByValue(float value) => this[value];
}