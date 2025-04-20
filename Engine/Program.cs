using _2DGameFramework.Core;
using _2DGameFramework.Core.Factories;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Core.Objects;
using _2DGameFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

#region Framework Startup
var provider = GameFramework.Start("config.xml", "2DGameFramework");

// Core serices
var logger              = provider.GetRequiredService<ILogger>();
var trapFactory         = provider.GetRequiredService<ITrapFactory>();
var creatureFactory     = provider.GetRequiredService<ICreatureFactory>();

// Generic factories
var armorFactory        = provider.GetRequiredService<IFactory<IArmor>>();
var weaponFactory       = provider.GetRequiredService<IFactory<IWeapon>>();
var consumableFactory   = provider.GetRequiredService<IFactory<IUsable>>();

logger.Log(TraceEventType.Information, LogCategory.Game, "Demo starting...");
#endregion

#region Register Built‑In Framework Items
// Weapons
weaponFactory.Register("RustySword", () => new ConfigurableWeapon(
    name:               "Rusty Sword",
    description:        "This sword has seen better days",
    hitdamage:          10,
    range:              1,
    weaponType:         WeaponType.OneHanded));

weaponFactory.Register("Bow", () => new ConfigurableWeapon(
    name: "Bow",
    description: "A simple wooden bow",
    hitdamage: 7,
    range: 5,
    weaponType: WeaponType.TwoHanded));

// Armor
armorFactory.Register("Helmet", () => new ConfigurableArmor(
    name:               "Basic Helmet",
    description:        "Simple steel helmet protecting the head.",
    damageReduction:    2,
    itemSlot:           ItemSlot.Head));

armorFactory.Register("Chestplate", () => new ConfigurableArmor(
    name:               "Leather Chestplate",
    description:        "Light leather armor for the torso.",
    damageReduction:    5,
    itemSlot:           ItemSlot.Torso));

armorFactory.Register("Greaves", () => new ConfigurableArmor(
    name:               "Iron Greaves",
    description:        "Sturdy greaves to protect the legs.",
    damageReduction:    4,
    itemSlot:           ItemSlot.Legs));

armorFactory.Register("Gauntlets", () => new ConfigurableArmor(
    name:               "Chainmail Gauntlets",
    description:        "Interlinked steel rings for hand protection.",
    damageReduction:    3,
    itemSlot:           ItemSlot.Hands));

armorFactory.Register("Boots", () => new ConfigurableArmor(
    name:               "Traveler’s Boots",
    description:        "Reinforced leather boots for the feet.",
    damageReduction:    2,
    itemSlot:           ItemSlot.Feet));

// Consumables
consumableFactory.Register("SmallHealingPotion", () => new ConfigurableConsumable(
    name: "Small Healing Potion",
    description: "Heals 20 HP",
    effect: c => c.AdjustHitPoints(20),
    logger: logger));

consumableFactory.Register("WeakPoison", () => new ConfigurableConsumable(
    name: "Weak Poison",
    description: "Deals 10 HP damage",
    effect: c => c.AdjustHitPoints(-10),
    logger: logger));

// TODO: FIX
//var ragePotion = new Consumable(
//    "Rage Potion",
//    creature => creature.AddTemporaryDamageBoost(1.5),
//    "Increases damage output by 50% temporarily");
#endregion

#region Create World & entities
var world = provider.GetRequiredService<World>();

// Creatures 
var hero = creatureFactory.Create("Hero-Man", "The hero of all the lands", 100, new Position(3, 4));
var goblin = creatureFactory.Create("Goblin", "Scrawny little goblin", 50, new Position(5, 6));

// Traps
var spikeTrap = trapFactory.CreateTrap(
    name:               "Deadly Spike Pit",
    description:        "Watch your step!",
    damageAmount:       25,
    damageType:         DamageType.Physical,
    position:           new Position(2, 4),
    isRemovable:        false);

// TODO: FIX
//var chest = new Container(
//    name: "A Chest",
//    description: "An old chest",
//    position: new Position(4, 1),
//    logger: logger);

//var tree = new EnvironmentObject(
//    "A Tree",
//    "A tall and majestic Tree",
//    new Position(1, 3));

// Items
var sword =             weaponFactory.Create("RustySword");      
var bow =               weaponFactory.Create("Bow");               
var helmet =            armorFactory.Create("Helmet");          
var potion =            consumableFactory.Create("SmallHealingPotion");

// place into world
world.AddCreature(hero);
world.AddCreature(goblin);

world.AddObject(spikeTrap);
world.AddObject(new ItemWrapper(sword, new Position(2, 3), logger));
world.AddObject(new ItemWrapper(bow, new Position(2, 4), logger));
world.AddObject(new ItemWrapper(helmet, new Position(3, 2), logger));
world.AddObject(new ItemWrapper(potion, new Position(3, 3), logger));
#endregion

#region Demo Actions

#endregion

// TODO: FIX
//Console.WriteLine(world.GetObjects());


hero.Attack(goblin);
//hero.Loot(swordWrapper, world);
hero.UseItem(potion);
spikeTrap.ReactTo(hero);

Console.WriteLine(world);