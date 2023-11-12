

public enum EventName
{
    SwitchGameMode,
    PlayerTakesDmg,         // param1: float, damage value; param2: gameobject, go that deals the damage
    EnemyTakesDmg,          // param1: float, damage value; 
    PlayerTakeUpgrade,      // param1: float, increased damage value; 
    EnemyTakePollution,     // param1: float, increased damage value; param2: float, increased spawn speed. 
}

