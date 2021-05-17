using System;

public enum SpellRadiusType {
    Line,
    Box,
    Circle,
    Front
}

public enum SpellType {
    Damage,
    Debuff,
    Other
}

public enum GridMode {
    Move = 0,
    Attack = 1,
    Knockback = 2
}