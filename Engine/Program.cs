using _2DGameFramework.Combat;
using _2DGameFramework.Configuration;
using _2DGameFramework.Core;
using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Objects;
using _2DGameFramework.Logging;
using _2DGameFramework.Services;
using System.Diagnostics;


#region Configuration
// 1) Load configuration (world dimensions, game level, plus logging settings)
var loader = new ConfigurationLoader();
var (worldSettings, loggerSettings) = loader.Load("config.xml");

// 2) Set up the TraceSource for framework-wide logging
var trace = new TraceSource("2DGameFramework")
{
    // 2a) Use the log‑level from config to filter events
    Switch = { Level = loggerSettings.LogLevel }
};

// 2b) Wire up each listener defined in the config (or default to Console)
if (loggerSettings.Listeners.Count > 0)
{
    foreach (var ListenerConfig in loggerSettings.Listeners)
    {
        // Determine listener type and any extra settings (e.g. file path)
        TraceListener listener = ListenerConfig.Type switch
        {
            // Console output
            "Console" => new ConsoleTraceListener(),

            // File output, only if a <Path> setting is present
            "File" when ListenerConfig.Settings.TryGetValue("Path", out var path) => new TextWriterTraceListener(path),

            // Unknown types throw so you catch mis‑configured XML
            _ => throw new InvalidOperationException($"Unknown listener type '{ListenerConfig.Type}'")
        };

        // 2c) Apply the same filter to each listener
        listener.Filter = new EventTypeFilter(loggerSettings.LogLevel);
        trace.Listeners.Add(listener);
    }
}
else
{
    // 2d) Fallback: always have at least a console listener
    trace.Listeners.Add(new ConsoleTraceListener());
}
#endregion

#region Logger
//3) Create logger adapter
ILogger logger = new GameLoggerAdapter(trace);

// 3a) Log a startup message at Information level
logger.Log(TraceEventType.Information, LogCategory.Game, "Game starting...");
#endregion

#region Constructors
// 4) Instantiate core game objects
var world = new World(
    worldSettings,
    logger
    );

var inventory = new InventoryService(logger);
var damageCalculator = new DamageCalculator();

var hero = new Creature(
    name: "Lennie", 
    description: null, 
    hitpoints: 100, 
    startPosition: new Position(3, 4), 
    logger: logger,
    inventory: inventory,
    damageCalculator: damageCalculator
    );

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

Console.WriteLine(world.GetObjects());

hero.Loot(swordWrapper, world);
hero.Attack(hero);
hero.UseItem(smallHealingPotion);
hero.UseItem(smallHealingPotion);
deadlyTrap.ReactTo(hero);

Console.WriteLine(tree);
Console.WriteLine(chest);
Console.WriteLine(sword);
Console.WriteLine(swordWrapper);
