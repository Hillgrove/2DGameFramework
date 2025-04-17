using _2DGameFramework.Configuration;
using _2DGameFramework.Logging;
using _2DGameFramework.Models;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

var fileLogger = new TextWriterTraceListener("error.log")
{
    Filter = new EventTypeFilter(SourceLevels.All)
};
GameLogger.Trace.Listeners.Add(fileLogger);
GameLogger.Log(TraceEventType.Warning, LogCategory.Game, "Test log entry for file output", offset: 1);
// Set's the global filter for the tracesource
// GameLogger.Trace.Switch.Level = SourceLevels.Warning;

var config = ConfigurationLoader.Load("config.xml");
var world = new World(config.WorldWidth, config.WorldHeight, config.GameLevel);


var hero = new Creature("Lennie", null, 100, new Position(0, 0));

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

var sword = new Sword("Rusty Sword", 10, 5, "This sword has seen better days");
var swordWrapper = new ItemWrapper(sword, new Position(2, 3));
world.AddObject(swordWrapper);

Console.WriteLine(world.GetObjects().ToString());

hero.Loot(swordWrapper, world);
hero.Attack(hero);
hero.UseItem(smallHealingPotion);
hero.UseItem(smallHealingPotion);
deadlyTrap.ReactTo(hero);

Console.WriteLine();
Console.WriteLine();

var tree = new EnvironmentObject("A Tree", "A tall and majestic Tree", new Position(1, 3));
Console.WriteLine(tree);

var chest = new Container("A Chest", "An old chest", new Position(4, 1));
Console.WriteLine(chest);

Console.WriteLine(sword);
Console.WriteLine(swordWrapper);