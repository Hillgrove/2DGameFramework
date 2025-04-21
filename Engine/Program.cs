using _2DGameFramework;
using _2DGameFramework.Core;
using _2DGameFramework.Domain.Combat;
using _2DGameFramework.Domain.Creatures;
using _2DGameFramework.Domain.Items.Decorators;
using _2DGameFramework.Domain.Items.Defaults;
using _2DGameFramework.Domain.Objects;
using _2DGameFramework.Domain.World;
using _2DGameFramework.Factories;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

#region Framework Startup
var provider = GameFramework.Start("config.xml", "2DGameFramework");

// Core serices
var logger =            provider.GetRequiredService<ILogger>();
var trapFactory =       provider.GetRequiredService<ITrapFactory>();
var creatureFactory =   provider.GetRequiredService<ICreatureFactory>();
var combatService =     provider.GetRequiredService<ICombatService>();

// Generic factories
var armorFactory =      provider.GetRequiredService<IFactory<IArmor>>();
var weaponFactory =     provider.GetRequiredService<IFactory<IWeapon>>();
var consumableFactory = provider.GetRequiredService<IFactory<IConsumable>>();

logger.Log(TraceEventType.Information, LogCategory.Game, "Demo starting...");
#endregion

#region Register Built‑In Framework Items
// Weapons
weaponFactory.Register("RustySword", () => new DefaultWeapon(
    name:               "Rusty Sword",
    description:        "This sword has seen better days",
    hitdamage:          10,
    range:              1,
    weaponType:         WeaponType.OneHanded));

weaponFactory.Register("ShinyDagger", () => new DefaultWeapon(
    name:               "Shiny Dagger",
    description:        "Not really that shiny to be honest",
    hitdamage:          5,
    range:              1,
    weaponType:         WeaponType.OneHanded));

weaponFactory.Register("Bow", () => new DefaultWeapon(
    name:               "Bow",
    description:        "A simple wooden bow",
    hitdamage:          7,
    range:              5,
    weaponType:         WeaponType.TwoHanded));

// Armor
armorFactory.Register("Helmet", () => new DefaultArmor(
    name:               "Basic Helmet",
    description:        "Simple steel helmet protecting the head.",
    damageReduction:    2,
    itemSlot:           ItemSlot.Head));

armorFactory.Register("Chestplate", () => new DefaultArmor(
    name:               "Leather Chestplate",
    description:        "Light leather armor for the torso.",
    damageReduction:    5,
    itemSlot:           ItemSlot.Torso));

armorFactory.Register("Greaves", () => new DefaultArmor(
    name:               "Iron Greaves",
    description:        "Sturdy greaves to protect the legs.",
    damageReduction:    4,
    itemSlot:           ItemSlot.Legs));

armorFactory.Register("Gauntlets", () => new DefaultArmor(
    name:               "Chainmail Gauntlets",
    description:        "Interlinked steel rings for hand protection.",
    damageReduction:    3,
    itemSlot:           ItemSlot.Hands));

armorFactory.Register("Boots", () => new DefaultArmor(
    name:               "Traveler’s Boots",
    description:        "Reinforced leather boots for the feet.",
    damageReduction:    2,
    itemSlot:           ItemSlot.Feet));

// Consumables
consumableFactory.Register("SmallHealingPotion", () => new DefaultConsumable(
    name:               "Small Healing Potion",
    description:        "Heals 20 HP",
    type:               ConsumableType.Healing,
    effect:             c => c.AdjustHitPoints(20),
    logger:             logger));

consumableFactory.Register("WeakPoison", () => new DefaultConsumable(
    name:               "Weak Poison",
    description:        "Deals 10 HP damage",
    type:               ConsumableType.Damage,
    effect:             c => c.AdjustHitPoints(-10),
    logger:             logger));
#endregion

#region Create World & entities
var world = provider.GetRequiredService<GameWorld>();

// Creatures 
var hero = creatureFactory.Create("Hero", "The hero of all the lands", 100, new Position(0, 0));
var goblin = creatureFactory.Create("Goblin", "Scrawny little goblin", 50, new Position(0, 1));

// Traps
var spikeTrap = trapFactory.CreateTrap(
    name:               "Deadly Spike Pit",
    description:        "Watch your step!",
    damageAmount:       25,
    damageType:         DamageType.Physical,
    position:           new Position(2, 4),
    isRemovable:        false);

// Containers
var chest = new Container(
    name:               "A Chest",
    description:        "An old chest",
    position:           new Position(4, 1),
    logger:             logger);

// Environement Objects
var tree = new EnvironmentObject(
    name:               "A Tree",
    description:        "A tall and majestic Tree",
    position:           new Position(1, 3));

// Items
var sword =             weaponFactory.Create("RustySword");
var dagger =            weaponFactory.Create("ShinyDagger");
var bow =               weaponFactory.Create("Bow");               
var helmet =            armorFactory.Create("Helmet");          
var potion =            consumableFactory.Create("SmallHealingPotion");

var oiledSword = new TimedWeaponDecorator(
    sword,
    baseDmg => baseDmg + 5,
    uses: 3);

// Composite attack
IAttackAction swordAttack = new DamageSourceAttack(sword, combatService);
IAttackAction daggerAttack = new DamageSourceAttack(dagger, combatService);

var dualWieldAttack = new CompositeAttackAction();
dualWieldAttack.Add(swordAttack);
dualWieldAttack.Add(daggerAttack);
#endregion

#region Fill Container
chest.AddItem(helmet);
chest.AddItem(potion);
#endregion

#region Populate World
// place into world
world.AddCreature(hero);
world.AddCreature(goblin);
world.AddObject(chest);
world.AddObject(spikeTrap);
world.AddObject(new ItemWrapper(bow, new Position(2, 4), logger));
world.AddObject(new ItemWrapper(helmet, new Position(3, 2), logger));
world.AddObject(new ItemWrapper(potion, new Position(3, 3), logger));
#endregion

#region Demo Actions
// TODO: FIX
//Console.WriteLine(world.GetObjects());

hero.Loot(chest, world); // auto-equips looted items
goblin.EquipWeapon(dagger);

hero.Attack(goblin);
goblin.Attack(hero);

var concreteHero = (DefaultCreature)hero;
concreteHero.AddAttackAction(dualWieldAttack);
concreteHero.Attack(goblin);

//hero.Attack(goblin);
spikeTrap.ReactTo(hero);
spikeTrap.ReactTo(hero);
spikeTrap.ReactTo(goblin);
spikeTrap.ReactTo(goblin);

Console.WriteLine(world);
#endregion

