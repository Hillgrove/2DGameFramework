using _2DGameFramework.Configuration;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using _2DGameFramework.Models;
using _2DGameFramework.Models.Base;
using System.Diagnostics;


#region Logging
// Create and attach file listener
var fileListener = new TextWriterTraceListener("error.log")
{
    Filter = new EventTypeFilter(SourceLevels.All)
};
GameLogger.Trace.Listeners.Add(fileListener);

// Testing Logger
GameLogger.Log(
    TraceEventType.Information, 
    LogCategory.Game,
    "Startup logged");

// Set's the global filter for the tracesource
// GameLogger.Trace.Switch.Level = SourceLevels.Warning;

// Create loggeradapter
ILogger logger = new GameLoggerAdapter();
#endregion

#region Configuration
// Load config
var loader = new ConfigurationLoader(logger);
var config = loader.Load("config.xml");

#endregion

#region Constructors
var world = new World(
    width: config.WorldWidth,
    height: config.WorldHeight,
    logger: logger,
    level: config.GameLevel);

var hero = new Creature(
    name: "Lennie", 
    description: null, 
    hitpoints: 100, 
    startPosition: new Position(3, 4), 
    logger: logger);

var smallHealingPotion = new Consumable(
    name: "Small Healing Potion",
    effect: creature => creature.Heal(20),
    description: "Heals for 20 HP",
    logger: logger);

var poisonVial = new Consumable(
    name: "Weak Poison",
    effect: creature => creature.ReceiveDamage(10),
    description: "Deals 10 HP damage",
    logger: logger);

var deadlyTrap = new Trap(
    name: "Deadly Trap",
    description: null,
    damageAmount: 50,
    logger: logger,
    position: new Position(2, 4));

var sword = new Sword(
    name: "Rusty Sword", 
    hitdamage: 10, 
    range: 5, 
    description: "This sword has seen better days");

var chest = new Container(
    name: "A Chest", 
    description: "An old chest",
    position: new Position(4, 1),
    logger: logger);

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
var swordWrapper = new ItemWrapper(
    sword,
    new Position(2, 3),
    logger: logger);

world.AddObject(swordWrapper);
world.AddObject(tree);
world.AddObject(chest);
#endregion

Console.WriteLine(world.GetObjects().ToString());

hero.Loot(swordWrapper, world);
hero.Attack(hero);
hero.UseItem(smallHealingPotion);
hero.UseItem(smallHealingPotion);
deadlyTrap.ReactTo(hero);

Console.WriteLine(tree);
Console.WriteLine(chest);
Console.WriteLine(sword);
Console.WriteLine(swordWrapper);