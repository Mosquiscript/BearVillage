//Enums
//Estaciones del año en el juego
public enum Season
{
    Primavera,
    Verano,
    Otoño,
    Invierno,
    none,
    count
}
//Profeciones de los aldeanos
public enum Profession
{
    Blacksmith,
    Miner,
    Woodcutter,
    Builder,
    Warrior,
    Archer
}
//Estado los aldenos
public enum State
{
    Idle,
    MovingToResourcePosition,
    GathererResource,
    MovingToStorage,
    Mining,
    CreatingTools,
    Fellingtree,
    Building,
    Attacking
}
//Tipo de recursos
public enum ResourceType
{
    Wood,
    Mine,
    Food
}
