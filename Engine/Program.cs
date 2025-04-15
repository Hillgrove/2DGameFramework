using _2DGameFramework;
using _2DGameFramework.Models;

var world = new World(10, 10);
var hero = new Creature("Lennie", 100, new Position(0, 0));


var smallHealingPotion = new Consumable(
    name: "Small Healing Potion",
    effect: creature => creature.Heal(20),
    description: "Heals for 20 HP");


var poisonVial = new Consumable(
    name: "Weak Poison",
    effect: creature => creature.ReceiveDamage(10),
    description: "Deals 10 HP damage");

//var ragePotion = new Consumable(
//    "Rage Potion",
//    creature => creature.AddTemporaryDamageBoost(1.5),
//    "Increases damage output by 50% temporarily");

var deadlyTrap = new Trap(
    name: "Deadly Trap",
    description: null,
    damageAmount: 50,
    position: new Position(2, 4));

//var sword = new WeaponBase("Rusty Sword", WeaponType.OneHanded, 1, 10, "This sword has seen better days");
//var swordWrapper = new ItemWrapper(sword, new Position(2, 3));
//world.AddObject(swordWrapper);

Console.WriteLine(world.GetObjects().ToString());


hero.ReceiveDamage(50);
Console.WriteLine(hero.Hitpoints);
hero.UseItem(smallHealingPotion);
hero.UseItem(smallHealingPotion);
Console.WriteLine(hero.Hitpoints);
deadlyTrap.ReactTo(hero);
Console.WriteLine(hero.Hitpoints);