using UnityEngine;
using System.Collections;

public static class SpellMethods {
    public static void DamageEnemy(CharacterController enemy) {
        enemy.DecreaseHealth();
    }

    public static void BlockTiles(Tile tile) {
        tile.defaultWeight = 15;
    }
}
