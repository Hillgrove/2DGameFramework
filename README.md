# 2DGameFramework – Mini-framework til turbaseret 2D-spil

### 4. Semester Obligatorisk Opgave – Forår 2025  

**Valgfag:** Advanced Software Construction


Dette bibliotek giver et grundlæggende API til at definere et turbaseret 2D-spilunivers uden GUI, med væsener, objekter, angreb og forsvarskomponenter.

---

## ⚙️ Funktionalitet 

- **🌍 World**: Konfigurerbar 2D-verden med bredde, højde og lister af væsener og objekter  
- **🐉 Creature**: Har navn, livspoint, angrebs- og forsvarsinventar samt metoder til at angribe, modtage skade og loot  
- **🎲 WorldObject**: Faste eller lootbare objekter, som kan give bonusser eller tilbageholde straffe  
- **⚔️ AttackItem & 🛡️ DefenseItem**: Våben eller magiske genstande med rækkevidde, skade- eller beskyttelseseffekt  

---

## 🔧 Udvidelser & Fleksibilitet 

- **🛠️ Konfiguration**: Læs konfigurationsfil (`config.xml`) for at sætte world size, difficulty level og logging
- **📋 Logging**: Brug `System.Diagnostics.TraceSource` med fleksible `TraceListener`-opsætninger (Console, File, filterniveauer)
- **📑 Dokumentation**: Offentlige metoder dokumenteres med XML-kommentarer (///)  
- **📐 SOLID & LINQ**: Følger SOLID-principper med eksempler på Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation og Dependency Inversion; anvender LINQ-iterationer  
- **🧩 Design Patterns**:  
  - **Template Method** til creature-logik  
  - **Observer** ved skadehændelser  
  - **Decorator** til at forstærke/weaken våben  
  - **Composite** for at samle flere items  
  - **Strategy** til bevægelses- eller angrebsalgoritmer  

---

## 💻 Teknologi 

- **Sprog**: C# (.NET)  
- **Logging**: `GameLoggerAdapter` wrapping `TraceSource`  
- **Konfiguration**: `ConfigurationLoader` håndterer XML-filen  
- **Arkitektur**: Interfaces, generiske factories, observer- og decorator-mønstre  

---

## 🚀 Eksempel på brug 

Start frameworket og hent services via DI:

```csharp
GameFramework.Start(configFilePath, traceSourceName);
var creatureFactory = serviceProvider.GetService<ICreatureFactory>();
var world = serviceProvider.GetService<World>();
