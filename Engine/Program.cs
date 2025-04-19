using _2DGameFramework.Core;
using _2DGameFramework.Core.Factories;
using _2DGameFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

#region Framework Startup
var provider = GameFramework.Start();
var frameworkLogger = provider.GetRequiredService<ILogger>();
frameworkLogger.Log(TraceEventType.Information, LogCategory.Game, "Framework starting...");

var trapFactory = provider.GetRequiredService<ITrapFactory>();
var armorFactory = provider.GetRequiredService<IArmorFactory>();
var weaponFactory = provider.GetRequiredService<IWeaponFactory>();
var creatureFactory = provider.GetRequiredService<ICreatureFactory>();
var consumableFactory = provider.GetRequiredService<IConsumableFactory>();
#endregion

#region Create World and Creatures
var world = provider.GetRequiredService<World>();
var hero = creatureFactory.Create("Hero-Man", "The hero of all the lands", 100, new Position(3, 4));
var goblin = creatureFactory.Create("Goblin", "Scrawny little goblin", 50, new Position(5, 6));
#endregion

#region Creating of other objects
var smallHealingPotion = consumableFactory.CreateConsumable(
    name: "Small Healing Potion",
    effect: creature => creature.AdjustHitPoints(20),
    description: "Heals for 20 HP");

var poisonVial = consumableFactory.CreateConsumable(
    name: "Weak Poison",
    effect: creature => creature.AdjustHitPoints(-10),
    description: "Deals 10 HP damage");

var deadlyTrap = trapFactory.CreateTrap(
    name: "Deadly Trap with no description",
    damageAmount: 50,
    position: new Position(2, 4));

var RemovableTrap = trapFactory.CreateTrap(
    name: "Removale Trap",
    description: "With a description",
    damageAmount: 50,
    position: new Position(2, 4),
    isRemovable: true);

var sword = weaponFactory.CreateSword(
    name: "Rusty Sword",
    hitdamage: 10,
    range: 5,
    description: "This sword has seen better days");

//var chest = new Container(
//    name: "A Chest",
//    description: "An old chest",
//    position: new Position(4, 1),
//    logger: logger);

var tree = new EnvironmentObject(
    "A Tree", 
    "A tall and majestic Tree", 
    new Position(1, 3));

//var ragePotion = new Consumable(
//    "Rage Potion",
//    creature => creature.AddTemporaryDamageBoost(1.5),
//    "Increases damage output by 50% temporarily");
#endregion

#region Adding Objects to world
//var swordWrapper = new ItemWrapper(
//    sword,
//    new Position(2, 3),
//    logger: logger);

//world.AddObject(swordWrapper);
//world.AddObject(tree);
//world.AddObject(chest);
#endregion

//Console.WriteLine(world.GetObjects());

//hero.Loot(swordWrapper, world);
hero.Attack(goblin);
hero.UseItem(smallHealingPotion);
hero.UseItem(smallHealingPotion);
deadlyTrap.ReactTo(hero);

Console.WriteLine(tree);
//Console.WriteLine(chest);
Console.WriteLine(sword);
//Console.WriteLine(swordWrapper);
