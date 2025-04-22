using UnityEngine;

public static class DragSettings
{

    //TODO ver se � possivel alterar no inspector no unity

    //Possui todas as variaveis do dragObject, e ao se alterar aqui alterase para todos os objetos.
    [SerializeField] public static float pushPullSpeed = 0.035f;
    [SerializeField] public static float range = 10f;
    [SerializeField] public static float minDistance = 1f;
    [SerializeField] public static float dragSpeed = 10f;
    [SerializeField] public static float damping = 10f;
    [SerializeField] public static float rotationSpeed = 5f;
    [SerializeField] public static float hp = 2;
}
