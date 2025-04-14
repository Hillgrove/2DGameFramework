using _2DGameFramework;
using _2DGameFramework.Models;
using _2DGameFramework.Models.Base;

var world = new World(10, 10);
var hero = new Creature("Lennie", 100, new Position(0, 0));
var hp20 = new HealingPotion("Healing Potion", 20);
var deadlyTrap = new Trap("Deadly Trap", 90);
var sword = new WeaponBase("Rusty Sword", WeaponType.OneHanded, 1, 10, "This sword has seen better days");
var swordWrapper = new ItemWrapper(sword, new Position(2, 3));
world.AddObject(swordWrapper);

Console.WriteLine(world.GetObjects().ToString());


hero.ReceiveDamage(50);
Console.WriteLine(hero.Hitpoints);
hero.UseItem(hp20);
hero.UseItem(hp20);
Console.WriteLine(hero.Hitpoints);
hero.UseItem(deadlyTrap);
Console.WriteLine(hero.Hitpoints);