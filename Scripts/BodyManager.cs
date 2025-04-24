using System;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    private bool hasBody = false;
    private bool hasEndedSurgery = true;
    private int mistakes = 0;
    private int organsPlaced = 0;

    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Transform bodySpawn;
    [SerializeField] private Transform surgeryPosition;
    [SerializeField] private Transform exitPosition;
    [SerializeField] private Transform trashPosition;

    private GameObject currentBody;

    private enum BodyState { None, MovingToSurgery, InSurgery, MovingOut, Failed }
    private BodyState state = BodyState.None;

    private float moveSpeed = 2f;

    public void OnOrganPlaced(bool broken)
    {
        if (broken)
        {
            mistakes++;
        }

        if (mistakes >= 2)
        {
            state = BodyState.Failed;
        }
        if (AllOrgansPlaced())
        {
            hasEndedSurgery = true;
            state = BodyState.MovingOut;
        }
        if (!broken)
        {
            organsPlaced += 1;
        }
    }

    private bool AllOrgansPlaced()
    {
        if (organsPlaced ==2)
        {
            return true;
        }
        return false;
    }

    private void deployBody()
    {
        if (hasEndedSurgery)
        {
            currentBody = Instantiate(bodyPrefab, bodySpawn.position, Quaternion.identity);
            hasBody = true;
            hasEndedSurgery = false;
            mistakes = 0;
            state = BodyState.MovingToSurgery;
        }
    }

    private void moveBody()
    {
        switch (state)
        {
            case BodyState.MovingToSurgery:
                MoveTo(surgeryPosition.position, () => state = BodyState.InSurgery);
                break;

            case BodyState.MovingOut:
                MoveTo(exitPosition.position, () =>
                {
                    GameManager.Instance.GetComponent<PlayerScript>().addMoney(10);
                    GameManager.Instance.NextBodyReady();
                    hasBody = false;
                    Destroy(currentBody);
                });
                break;

            case BodyState.Failed:
                MoveTo(trashPosition.position, () =>
                {
                    GameManager.Instance.addBodyCounter();
                    GameManager.Instance.FailBody();
                    GameManager.Instance.NextBodyReady();
                    hasBody = false;
                    Destroy(currentBody);
                });
                break;
        }
    }

    private void MoveTo(Vector3 target, Action onArrive)
    {
        if (currentBody == null) return;

        currentBody.transform.position = Vector3.MoveTowards(
            currentBody.transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(currentBody.transform.position, target) < 0.01f)
        {
            onArrive.Invoke();
        }
    }

    private void Update()
    {
        if (hasBody)
        {
            moveBody();
        }
        else
        {

            deployBody();
        }
    }
}
