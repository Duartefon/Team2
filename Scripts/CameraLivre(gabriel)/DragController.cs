using System.ComponentModel;
using UnityEngine;

class DragController : MonoBehaviour
{

    private Transform cameraTransform;
    private RaycastHit hit;
    private Rigidbody rb;
    private bool isDragging;
    private float currentDistance;
    private Vector3 grabPos;



    void Start()
    {
        // guarda-se o transform da camera
        cameraTransform = transform;
    }

    void Update()
    {
        PushPull();

        if (Input.GetMouseButtonDown(0))
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    void FixedUpdate()
    {
        if (isDragging && rb != null)
        {
            //calcula a posição para onde queremos que o objeto se mova
            Vector3 targetPos = cameraTransform.position + cameraTransform.forward * currentDistance + grabPos;
            //calcula a velocidade que queremos que o objeto tenha até chegar à posição que queremos (targetPos)
            Vector3 desiredVelocity = (targetPos - rb.position) * DragSettings.dragSpeed;

            //aplica a velocidade com o lerp para ser mais smooth
            /*rb.linearVelocity = Vector3.Lerp(
                rb.linearVelocity,
                desiredVelocity,
                DragSettings.damping * Time.fixedDeltaTime
            );*/
        }
    }


    void Drag()
    {
        // isto faz um raycast a partir do centro da ecrã
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, DragSettings.range)) // se atingiu algo
        {
            // verifica se o objeto tem um rigidbody
            rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                isDragging = true;
                //calcula a distancia entre o objeto e a camera
                currentDistance = Vector3.Distance(hit.point, cameraTransform.position);
                //calcula a posicao do objeto que vai ser pego
                grabPos = rb.position - (cameraTransform.position + cameraTransform.forward * currentDistance);

            }
        }
    }

    void EndDrag()
    {
        isDragging = false;
        rb = null;
    }

    void PushPull()
    {
        if (isDragging && rb != null)
        {
            // isto usa o W e o S mas acho que com o scroll seria melhor
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                currentDistance += scroll * DragSettings.pushPullSpeed;
                currentDistance = Mathf.Clamp(currentDistance, DragSettings.minDistance, DragSettings.range);
            }
        }

    }


}