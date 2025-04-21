using UnityEngine;

public class MoverUI : MonoBehaviour
{
    public RectTransform contenedorCorazones;  
    public Camera mainCamera;  

    void Update()
    {
        Vector3 camPos = mainCamera.transform.position;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(camPos);

        contenedorCorazones.position = new Vector3(screenPos.x - Screen.width / 2, screenPos.y - Screen.height / 2, 0);

        // Si deseas que los corazones se mantengan en una posición fija en la pantalla (por ejemplo, siempre en la esquina inferior izquierda), puedes hacerlo de la siguiente manera:
        // contenedorCorazones.anchorMin = new Vector2(0, 0);  // Esquina inferior izquierda
        // contenedorCorazones.anchorMax = new Vector2(0, 0);  // Esquina inferior izquierda
        // contenedorCorazones.anchoredPosition = new Vector2(10, 10); // Ajusta la distancia desde el borde
    }
}
