using _2DGameFramework.Models;

Creature hero = new("Lennie", 100, new Position(0, 0));

hero.ReceiveDamage(50);

Console.WriteLine(hero.Hitpoints);

HealingPotion hp20 = new("Healing potion", 20);
Trap deadlyTrap = new("Deadly Trap", 90);

hero.UseItem(hp20);
Console.WriteLine(hero.Hitpoints);

hero.UseItem(hp20);
Console.WriteLine(hero.Hitpoints);

hero.UseItem(hp20);
Console.WriteLine(hero.Hitpoints);

hero.UseItem(deadlyTrap);
Console.WriteLine(hero.Hitpoints);