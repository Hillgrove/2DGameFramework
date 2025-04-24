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


#region Framework Startup
Console.WriteLine("Framework Startup...");

var provider = GameFramework.Start("config.xml", "2DGameFramework");

// Core serices
var logger = provider.GetRequiredService<ILogger>();
var trapFactory = provider.GetRequiredService<ITrapFactory>();
var combatService = provider.GetRequiredService<ICombatService>();
var creatureFactory = provider.GetRequiredService<ICreatureFactory>();

// Generic factories
var armorFactory = provider.GetRequiredService<IFactory<IArmor>>();
var weaponFactory = provider.GetRequiredService<IFactory<IWeapon>>();
var consumableFactory = provider.GetRequiredService<IFactory<IConsumable>>();

// World
var world = provider.GetRequiredService<GameWorld>();
#endregion

#region Register Weapons
Console.WriteLine("\nRegistering Weapons...");

weaponFactory.Register("RustySword", () => new DefaultWeapon(
    name: "Rusty Sword",
    description: "This sword has seen better days",
    hitdamage: 10,
    range: 1,
    weaponType: WeaponType.OneHanded));

weaponFactory.Register("ShinyDagger", () => new DefaultWeapon(
    name: "Shiny Dagger",
    description: "Not really that shiny to be honest",
    hitdamage: 5,
    range: 1,
    weaponType: WeaponType.OneHanded));

weaponFactory.Register("LongBow", () => new DefaultWeapon(
    name: "Bow",
    description: "A simple wooden bow",
    hitdamage: 7,
    range: 5,
    weaponType: WeaponType.TwoHanded));
#endregion

#region Register Armor
Console.WriteLine("\nRegistering Armor...");

armorFactory.Register("Helmet", () => new DefaultArmor(
    name: "Basic Helmet",
    description: "Simple steel helmet protecting the head.",
    damageReduction: 2,
    itemSlot: ItemSlot.Head));

armorFactory.Register("Chestplate", () => new DefaultArmor(
    name: "Leather Chestplate",
    description: "Light leather armor for the torso.",
    damageReduction: 5,
    itemSlot: ItemSlot.Torso));

armorFactory.Register("Greaves", () => new DefaultArmor(
    name: "Iron Greaves",
    description: "Sturdy greaves to protect the legs.",
    damageReduction: 4,
    itemSlot: ItemSlot.Legs));

armorFactory.Register("Gauntlets", () => new DefaultArmor(
    name: "Chainmail Gauntlets",
    description: "Interlinked steel rings for hand protection.",
    damageReduction: 3,
    itemSlot: ItemSlot.Hands));

armorFactory.Register("Boots", () => new DefaultArmor(
    name: "Traveler’s Boots",
    description: "Reinforced leather boots for the feet.",
    damageReduction: 2,
    itemSlot: ItemSlot.Feet));
#endregion

#region Register Consumables
Console.WriteLine("\nRegistering Consumables...");

consumableFactory.Register("SmallHealingPotion", () => new DefaultConsumable(
    name: "Small Healing Potion",
    description: "Heals 20 HP",
    type: ConsumableType.Healing,
    effect: c => c.AdjustHitPoints(20),
    logger: logger));

consumableFactory.Register("WeakPoison", () => new DefaultConsumable(
    name: "Weak Poison",
    description: "Deals 10 HP damage",
    type: ConsumableType.Damage,
    effect: c => c.AdjustHitPoints(-10),
    logger: logger));
#endregion

#region Create Creatures

Console.WriteLine("\nCreating Creatures...");
var hero = creatureFactory.Create("Hero", "The hero of all the lands", 100, new Position(2, 2));
var goblin = creatureFactory.Create("Goblin", "Scrawny little goblin", 50, new Position(1, 1), .50);
#endregion

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("               **********************************");
Console.WriteLine("               *                                *");
Console.WriteLine("               *   Testing of 2DGameFramework   *");
Console.WriteLine("               *                                *");
Console.WriteLine("               **********************************");

#region Container & Loot Tests
Console.WriteLine("\n========================= Container =======================\n");
var chest = new Container(
    name: "A Chest",
    description: "A dusty wooden chest",
    position: new Position(2, 2),
    logger: logger);

// Place items in chest
var sword = weaponFactory.Create("RustySword");
var potion = consumableFactory.Create("SmallHealingPotion");
var poison = consumableFactory.Create("WeakPoison");
var helmet = armorFactory.Create("Helmet");

chest.AddItem(sword);
chest.AddItem(helmet);
chest.AddItem(potion);

Console.WriteLine();
goblin.Inventory.AddItem(poison);
goblin.EquipArmor(helmet);

Console.WriteLine($"\n{ chest}");
Wait();

Console.WriteLine("\n=========================== Loot ==========================\n");
// Hero loots chest
hero.Loot(chest, world);
Console.WriteLine("\nRemaining in chest: " + chest.PeekLoot().Count() + " items");
Wait();


// Add poison and loot again
 Console.WriteLine();
chest.AddItem(poison);
hero.Loot(chest, world);
Console.WriteLine("\nAfter second loot, hero has these usables: " + string.Join(", ", hero.GetUsables().Select(u => u.Name)));

Wait();
#endregion

#region Consumables
Console.WriteLine("\n======================= Consumables =======================\n");
Console.WriteLine($"Hero HP before use: {hero.HitPoints}\n");

var damagePoison = hero.GetUsables().FirstOrDefault(u => u.Name == "Weak Poison");
if (damagePoison != null) hero.UseItem(damagePoison);
Console.WriteLine($"Hero HP after poison: {hero.HitPoints}\n");

var healingPotion = hero.GetUsables().FirstOrDefault(u => u.Name == "Small Healing Potion");
if (healingPotion != null) hero.UseItem(healingPotion);
Console.WriteLine($"Hero HP after healing: {hero.HitPoints}");

Wait();
#endregion

#region World objects
Console.WriteLine("\n====================== World Objects ======================\n");

// Traps
var spikeTrap = trapFactory.CreateTrap(
    name: "Deadly Spike Pit",
    description: "Watch your step!",
    damageAmount: 100,
    damageType: DamageType.Physical,
    position: new Position(2, 4),
    isRemovable: false);

// Environement Objects
var tree = new EnvironmentObject(
    name: "A Tree",
    description: "A tall and majestic Tree",
    position: new Position(1, 3));

world.AddCreature(hero);
world.AddCreature(goblin);
world.AddObject(chest);
world.AddObject(spikeTrap);
world.AddObject(tree);

// Show all creatures still alive
var alive = world.GetCreatures().Where(c => c.HitPoints > 0);
Console.WriteLine("\nAlive creatures in the world: " + string.Join(", ", alive.Select(c => c.Name)));

// Find objects at hero's position
var objectsAtHero = world.GetObjects().Where(o => o.Position.Equals(hero.Position));
Console.WriteLine("Objects at hero's position: " + string.Join(", ", objectsAtHero.Select(o => o.Name)));

Wait();
#endregion

#region Movement
Console.WriteLine("\n========================= Movement ========================\n");

hero.MoveBy(-2, -2, world);
goblin.MoveBy(-2, 1, world);

Wait();
#endregion

#region Attacks
Console.WriteLine("\n========================= Attack ==========================\n");

// Prepare attack actions using CombatService
IAttackAction swordAttack = new DamageSourceAttack(sword, combatService);
IAttackAction daggerAttack = new DamageSourceAttack(weaponFactory.Create("ShinyDagger"), combatService);
IAttackAction bowAttack = new DamageSourceAttack(weaponFactory.Create("LongBow"), combatService);

// TODO: Fix concrete hero
// Register actions with keys on DefaultCreature
var heroCreature = (DefaultCreature)hero;
heroCreature.AddAttackAction("Sword", swordAttack);
heroCreature.AddAttackAction("Dagger", daggerAttack);
heroCreature.AddAttackAction("Bow", bowAttack);

// Composite action (Dual Wield)
var dualWieldAction = new CompositeAttackAction();
dualWieldAction.Add(swordAttack);
dualWieldAction.Add(daggerAttack);
heroCreature.AddAttackAction("DualWield", dualWieldAction);

// Decorator test: timed sword buff
var timedSword = new TimedWeaponDecorator(sword, dmg => dmg + 5, uses: 2);
var buffedAttack = new DamageSourceAttack(timedSword, combatService);
heroCreature.AddAttackAction("BuffedSword", buffedAttack);


// Execute specific actions by key
Console.WriteLine("-- Sword Attack --");
heroCreature.Attack("Sword", goblin);
Console.ReadKey();

Console.WriteLine("\n-- Dagger Attack --");
heroCreature.Attack("Dagger", goblin);
Console.ReadKey();

Console.WriteLine("\n-- Bow Attack --");
heroCreature.Attack("Bow", goblin);
Console.ReadKey();

Console.WriteLine("\n-- Dual Wield Attack --");
heroCreature.Attack("DualWield", goblin);
Console.ReadKey();

Console.WriteLine("\n-- Buffed Sword Attack --");
heroCreature.Attack("BuffedSword", goblin);
 Console.ReadKey();

Console.WriteLine("\n-- Killing off Goblin with a trap --");
spikeTrap.ReactTo(goblin);

Wait();
#endregion

#region Helper Functions
static void Wait()
{
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}
#endregion

