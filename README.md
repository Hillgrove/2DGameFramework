# 2DGameFramework

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12-239120?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![DI](https://img.shields.io/badge/Microsoft.Extensions.DependencyInjection-9.0-0078D4?logo=microsoft)](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/)
[![License](https://img.shields.io/badge/licens-MIT-blue)](LICENSE)

Et turbaseret 2D-spil-framework skrevet i C# (.NET 8) — uden GUI. Frameworket stiller et komplet API til rådighed til at definere en spilleverden med væsener, genstande, kamp og bevægelse, og er designet til at være let at udvide via interfaces og dependency injection.

> Lavet som obligatorisk opgave på 4. semester, faget **Advanced Software Construction**, Forår 2025.

---

## Indholdsfortegnelse

- [Funktionalitet](#funktionalitet)
- [Projektstruktur](#projektstruktur)
- [Arkitektur & Design Patterns](#arkitektur--design-patterns)
- [Kom i gang](#kom-i-gang)
- [Konfiguration](#konfiguration)
- [Brug](#brug)

---

## Funktionalitet

| Område | Beskrivelse |
|---|---|
| **Verden** | Konfigurerbar 2D-verden (`GameWorld`) med bredde, højde og sværhedsgrad |
| **Væsener** | Abstrakt `Creature`-klasse med HP, position, inventar, kamp og bevægelse |
| **Våben** | `DefaultWeapon` med skade, rækkevidde og type (en-/tohånds) |
| **Rustning** | `DefaultArmor` med damage reduction per item slot (hoved, torso, ben, hænder, fødder) |
| **Forbrugsartikler** | `DefaultConsumable` med brugerdefineret effekt (healing, gift, m.m.) |
| **Containere & fælder** | `Container` (lootable), `Trap` (reaktiv), `EnvironmentObject` (statisk) |
| **Kamp** | `CombatService` beregner skade ud fra angriber og forsvarer |
| **Logging** | `GameLoggerAdapter` wrapper `System.Diagnostics.TraceSource` |
| **Konfiguration** | XML-baseret opsætning af verdensstørrelse, sværhedsgrad og log-lyttere |
| **DI** | Hele frameworket sættes op via `Microsoft.Extensions.DependencyInjection` |

---

## Projektstruktur

```
2DGameFramework/
├── 2DGameFramework/              ← Framework-biblioteket (class library)
│   ├── Configuration/            ← XML-konfiguration (ConfigurationLoader, WorldSettings)
│   ├── Core/                     ← Enums: DamageType, WeaponType, ItemSlot, GameLevel
│   ├── Domain/
│   │   ├── Combat/               ← IAttackAction, DamageSourceAttack, CompositeAttackAction
│   │   ├── Creatures/            ← Creature (abstrakt), DefaultCreature
│   │   ├── Items/
│   │   │   ├── Base/             ← ItemBase, WeaponBase, ArmorBase
│   │   │   ├── Decorators/       ← WeaponDecorator, TimedWeaponDecorator, DamageSourceDecorator
│   │   │   └── Defaults/         ← DefaultWeapon, DefaultArmor, DefaultConsumable
│   │   ├── Objects/              ← Container, Trap, ItemWrapper
│   │   └── World/                ← GameWorld, EnvironmentObject, WorldObject
│   ├── Extensions/               ← ServiceCollectionExtensions (DI-registrering)
│   ├── Factories/                ← Factory<T>, CreatureFactory, TrapFactory
│   ├── Interfaces/               ← Alle offentlige interfaces
│   ├── Logging/                  ← GameLogger, GameLoggerAdapter
│   ├── Observers/                ← HealthObserver, DeathObserver
│   ├── Services/                 ← CombatService, MovementService, InventoryService, m.fl.
│   └── GameFramework.cs          ← Entry point — Start() konfigurerer og returnerer ServiceProvider
└── Engine/                       ← Demo-applikation (DemoApp)
    ├── Program.cs                ← Eksempel på fuld brug af frameworket
    └── config.xml                ← Konfigurationsfil til demo
```

---

## Arkitektur & Design Patterns

Frameworket er bygget med SOLID-principper og demonstrerer fem klassiske design patterns.

### Template Method — angrebssekvens

`Creature` definerer en fast angrebssekvens via tre hooks. Subklasser overrider `DoAttack` og kan valgfrit tilpasse `PreAttack`/`PostAttack`.

```csharp
// Creature.cs
protected void AttackTemplate(IAttackAction action, ICreature target)
{
    PreAttack(action, target);   // f.eks. kontroller rækkevidde, forbrug stamina
    DoAttack(action, target);    // abstrakt — implementeres af subklassen
    PostAttack(action, target);  // f.eks. log skade, trigger bleed-effekt
}
```

```csharp
// DefaultCreature.cs
protected override void DoAttack(IAttackAction action, ICreature target)
    => action.Execute(this, target);
```

---

### Observer — HP-overvågning

`HealthObserver` lytter på et væsens `HealthChanged`-event og auto-healer, hvis HP falder under en given tærskel.

```csharp
var healthObserver = new HealthObserver(logger, hero.Inventory);
healthObserver.Subscribe(hero, thresholdFraction: 0.30); // auto-heal under 30% HP
```

`Creature` udsender selv `HealthChanged` og `OnDeath` ved HP-ændringer — observere kan abonnere uden at `Creature` kender dem.

---

### Decorator — midlertidige våbenbuffere

`TimedWeaponDecorator` wrapper et eksisterende våben og forstærker dets skade i et begrænset antal angreb.

```csharp
var timedSword = new TimedWeaponDecorator(
    inner: sword,
    modifier: dmg => dmg + 5,   // +5 skade per slag
    uses: 2);                   // virker kun de næste 2 angreb

heroCreature.AddAttackAction("BuffedSword", new DamageSourceAttack(timedSword, combatService));
```

---

### Composite — sammensatte angreb

`CompositeAttackAction` samler flere angrebshandlinger i én, som udføres sekventielt. Bruges til f.eks. dual wield.

```csharp
var dualWield = new CompositeAttackAction();
dualWield.Add(new DamageSourceAttack(sword, combatService));
dualWield.Add(new DamageSourceAttack(dagger, combatService));

heroCreature.AddAttackAction("DualWield", dualWield);
heroCreature.Attack("DualWield", goblin); // udfører begge angreb
```

---

### Strategy — udskiftelige angrebshandlinger

`IAttackAction` er strategien. Man registrerer navngivne handlinger på et væsen og vælger strategi ved kald.

```csharp
heroCreature.AddAttackAction("Sword",  new DamageSourceAttack(sword,  combatService));
heroCreature.AddAttackAction("Bow",    new DamageSourceAttack(bow,    combatService));

heroCreature.Attack("Sword", goblin);  // skifter strategi blot ved at ændre nøglen
heroCreature.Attack("Bow",   goblin);
```

---

### Factory — oprettelse og registrering af typer

Den generiske `Factory<T>` bruges til våben, rustning og forbrugsartikler. Man registrerer en fabriksfunktion under en nøgle og opretter instanser on demand.

```csharp
weaponFactory.Register("RustySword", () => new DefaultWeapon(
    name: "Rusty Sword",
    hitdamage: 10,
    range: 1,
    weaponType: WeaponType.OneHanded));

var sword = weaponFactory.Create("RustySword");
```

---

## Kom i gang

**Krav:**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- Visual Studio 2022 (eller `dotnet CLI`)

**Byg og kør demo-appen:**

```bash
# Klon repositoriet
git clone https://github.com/Hillgrove/2DGameFramework.git
cd 2DGameFramework

# Byg hele løsningen
dotnet build

# Kør demo-applikationen
dotnet run --project Engine
```

**Brug frameworket som library i dit eget projekt:**

Tilføj en project reference til `2DGameFramework/2DGameFramework.csproj` og start frameworket med:

```csharp
var provider = GameFramework.Start("config.xml", "MinSpilLogger");
```

---

## Konfiguration

Frameworket indlæser en XML-fil ved opstart. `Engine/config.xml` indeholder standardopsætningen:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Configuration>
    <WorldWidth>15</WorldWidth>
    <WorldHeight>15</WorldHeight>
    <GameLevel>Normal</GameLevel>

    <Logging>
        <GlobalSourceLevel>Information</GlobalSourceLevel>
        <Listeners>
            <!-- Logger alt til konsollen -->
            <Listener type="Console" />

            <!-- Logger Information og derover til fil -->
            <Listener type="File">
                <Path>game-info.log</Path>
            </Listener>

            <!-- Logger kun Warning og derover til separat fil -->
            <Listener type="File">
                <FilterLevel>Warning</FilterLevel>
                <Path>game-warning.log</Path>
            </Listener>
        </Listeners>
    </Logging>
</Configuration>
```

| Felt | Mulige værdier |
|---|---|
| `GameLevel` | `Easy`, `Normal`, `Hard` |
| `GlobalSourceLevel` | `Verbose`, `Information`, `Warning`, `Error`, `Critical` |
| `Listener type` | `Console`, `File` |

---

## Brug

Herunder er et samlet eksempel på at starte frameworket, registrere typer og køre grundlæggende spilscenarier.

```csharp
// 1. Start frameworket og hent services
var provider = GameFramework.Start("config.xml", "2DGameFramework");

var logger          = provider.GetRequiredService<ILogger>();
var world           = provider.GetRequiredService<GameWorld>();
var creatureFactory = provider.GetRequiredService<ICreatureFactory>();
var weaponFactory   = provider.GetRequiredService<IFactory<IWeapon>>();
var armorFactory    = provider.GetRequiredService<IFactory<IArmor>>();
var consumableFactory = provider.GetRequiredService<IFactory<IConsumable>>();
var combatService   = provider.GetRequiredService<ICombatService>();

// 2. Registrér typer i fabrikker
weaponFactory.Register("Sword", () => new DefaultWeapon("Sword", "A sharp blade", hitdamage: 12, range: 1, WeaponType.OneHanded));
armorFactory.Register("Helmet", () => new DefaultArmor("Helmet", "Basic protection", damageReduction: 3, ItemSlot.Head));
consumableFactory.Register("HealPotion", () => new DefaultConsumable("Healing Potion", "Heals 20 HP",
    ConsumableType.Healing, effect: c => c.AdjustHitPoints(20), logger));

// 3. Opret væsener og tilføj til verden
var hero   = creatureFactory.Create("Hero",   "The protagonist", 100, new Position(2, 2));
var goblin = creatureFactory.Create("Goblin", "A nasty creature",  50, new Position(3, 3));

world.AddCreature(hero);
world.AddCreature(goblin);

// 4. Udstyr og tilføj til inventar
var sword = weaponFactory.Create("Sword");
hero.EquipWeapon(sword);
hero.Inventory.AddItem(consumableFactory.Create("HealPotion"));

// 5. Opsæt observer — auto-heal under 40% HP
var healthObserver = new HealthObserver(logger, hero.Inventory);
healthObserver.Subscribe(hero, thresholdFraction: 0.40);

// 6. Registrér angrebshandlinger og angrib
var heroCreature = (DefaultCreature)hero;
heroCreature.AddAttackAction("Sword", new DamageSourceAttack(sword, combatService));
heroCreature.Attack("Sword", goblin);

// 7. Bevæg et væsen
hero.MoveBy(1, 0, world);

// 8. Tilføj en fælde til verden
var trapFactory = provider.GetRequiredService<ITrapFactory>();
var trap = trapFactory.CreateTrap("Spike Pit", "Ouch!", damageAmount: 30, DamageType.Physical, new Position(4, 4), isRemovable: false);
world.AddObject(trap);
trap.ReactTo(goblin); // aktivér fælden manuelt
```
