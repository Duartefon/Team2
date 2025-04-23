using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    private bool isHovering;
    private bool isDragging;
    private Outline outline;
    public float outlineWidth = 10f;
    public Color outlineColor = Color.yellow;
    public Color grabColor = new Color(1f, 0.5f, 0f); // cor do outline quando se dá drag
    // Enquanto dás drag a outline fica laranja
    void OnMouseDown()
    {
        if (isHovering)
        {
            isDragging = true;
            SetOutline(outlineWidth, grabColor);

        }
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
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        isHovering = Physics.Raycast(ray, out hit, DragSettings.range) && hit.collider.gameObject == gameObject;
    }

    void UpdateOutline()
    {
        if (isDragging)
        {
            SetOutline(outlineWidth, grabColor); // mantém vermelho se drag
        }
        else if (isHovering)
        {
            SetOutline(outlineWidth, outlineColor); // amarelo quando da hover
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