using UnityEngine;

public static class Options
{

    //TODO ver se � possivel alterar no inspector no unity

    //Possui todas as variaveis do dragObject, e ao se alterar aqui alterase para todos os objetos.
    [SerializeField] public static float dragSpeed = 8f;
    [SerializeField] public static float moveSpeedPercentage = 0.015f;

    [SerializeField] public static float maxDist = 5.5f;
    [SerializeField] public static float minDist = 1.5f;

    [SerializeField] public static float hp = 2;

    [SerializeField] public static float rotationSpeed = 70f;
}
