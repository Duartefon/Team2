using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    private bool isHovering;
    private bool isDragging;
    private Outline outline;

    // Enquanto dás drag a outline fica vermelha, eu queria laranja mas n tem predefenido dps faço se calhar
    void OnMouseDown()
    {
        isDragging = true;
        SetOutline(20f, Color.red);
    }

    // Deixas de dar drag tira o outline vermelho
    void OnMouseUp()
    {
        isDragging = false;
        UpdateOutline();
    }

    private void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineMode = Outline.Mode.OutlineAll;
    }

    private void Update()
    {
        CheckHover();
        UpdateOutline();
    }

    // faz um raycast a partir do centro da tela para ver se o cursor tá em cima do objeto
    void CheckHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;
        isHovering = Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject;
    }

    void UpdateOutline()
    {
        if (isDragging)
        {
            SetOutline(20f, Color.red); // mantém vermelho se drag
        }
        else if (isHovering)
        {
            SetOutline(20f, Color.yellow); // amarelo quando da hover
        }
        else
        {
            SetOutline(0f, Color.clear, false); // dá disable quando n da hover nem drag
        }
    }

    void SetOutline(float width, Color color, bool enable = true)
    {
        outline.enabled = enable;
        if (enable)
        {
            outline.OutlineWidth = width;
            outline.OutlineColor = color;
        }
    }
}