using _2DGameFramework;
using _2DGameFramework.Objects.Base;

EnvironmentObject treeObject = new("Tree", new Position(2, 4));
EnvironmentObject barrierObject = new("Barrier", new Position(4, 5), true);

ArmorBase helmetOne = new("Helmet of Gorgon", ArmorType.head, 20, DamageType.Physical, "A fancy helmet");
ArmorBase helmetTwo = new("Helmet of Gorgon", ArmorType.head, 20, DamageType.Fire, "A fancy helmet");
