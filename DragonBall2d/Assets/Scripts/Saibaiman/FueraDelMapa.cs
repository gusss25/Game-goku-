using UnityEngine;

/// <summary>
/// Este script permite que un objeto 2D se teletransporte al lado opuesto de la pantalla
/// cuando sale de los l�mites de la c�mara. Puede ser usado tanto para el jugador como para los enemigos.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class FueraDelMapa : MonoBehaviour
{
    // Referencia a la c�mara principal
    private Camera cam;

    // L�mites de la pantalla en coordenadas del mundo
    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;

    // Mitad del tama�o del sprite para un teletransporte m�s suave
    private float spriteHalfWidth;
    private float spriteHalfHeight;

    // Cooldown para evitar m�ltiples teletransportes en un corto per�odo de tiempo
    private float teleportCooldown = 0.2f; // Ajusta este valor para probar diferentes tiempos de cooldown
    private float lastTeleportTime = 0f;

    /// <summary>
    /// Inicializa las referencias y calcula los l�mites iniciales de la pantalla.
    /// </summary>
    void Start()
    {
        // Obtener la c�mara principal
        cam = Camera.main;

        // Obtener el SpriteRenderer para calcular el tama�o del sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            spriteHalfWidth = sr.bounds.size.x / 2;
            spriteHalfHeight = sr.bounds.size.y / 2;
        }
        else
        {
            Debug.LogWarning("WrapAround requiere un SpriteRenderer en el mismo GameObject.");
            spriteHalfWidth = 0.5f; // Valores por defecto
            spriteHalfHeight = 0.5f;
        }

        // Calcular los l�mites de la pantalla
        CalculateScreenBounds();
    }

    /// <summary>
    /// Recalcula los l�mites de la pantalla en cada frame para manejar c�maras m�viles o cambios de tama�o.
    /// </summary>
    void Update()
    {
        CalculateScreenBounds();

        // Solo permitir teletransporte si ha pasado suficiente tiempo desde el �ltimo
        if (Time.time - lastTeleportTime >= teleportCooldown)
        {
            WrapPosition();
        }
    }

    /// <summary>
    /// Calcula los l�mites de la pantalla en coordenadas del mundo, ajustando por el tama�o del sprite.
    /// </summary>
    void CalculateScreenBounds()
    {
        // Calcula el tama�o de la c�mara en unidades del mundo
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Define los l�mites izquierdo, derecho, superior e inferior ajustados por el tama�o del sprite
        screenLeft = cam.transform.position.x - camWidth / 2 - spriteHalfWidth;
        screenRight = cam.transform.position.x + camWidth / 2 + spriteHalfWidth;
        screenTop = cam.transform.position.y + camHeight / 2 + spriteHalfHeight;
        screenBottom = cam.transform.position.y - camHeight / 2 - spriteHalfHeight;
    }

    /// <summary>
    /// Verifica la posici�n del objeto y lo teletransporta al lado opuesto si es necesario.
    /// </summary>
    void WrapPosition()
    {
        Vector3 pos = transform.position;
        bool wrapped = false;

        // Teletransporte Horizontal
        if (pos.x > screenRight)
        {
            pos.x = screenLeft;
            wrapped = true;
        }
        else if (pos.x < screenLeft)
        {
            pos.x = screenRight;
            wrapped = true;
        }

        // Solo realiza el teletransporte vertical si no hubo uno horizontal
        if (!wrapped)
        {
            // Teletransporte Vertical
            if (pos.y > screenTop)
            {
                pos.y = screenBottom;
                wrapped = true;
            }
            else if (pos.y < screenBottom)
            {
                pos.y = screenTop;
                wrapped = true;
            }
        }

        if (wrapped)
        {
            transform.position = pos;
            lastTeleportTime = Time.time; // Reinicia el cooldown
        }
    }

    /// <summary>
    /// Opcional: Dibuja los l�mites de la pantalla en el editor para facilitar la visualizaci�n.
    /// </summary>
    void OnDrawGizmos()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            // Calcula los l�mites de la pantalla
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            float left = cam.transform.position.x - camWidth / 2 - spriteHalfWidth;
            float right = cam.transform.position.x + camWidth / 2 + spriteHalfWidth;
            float top = cam.transform.position.y + camHeight / 2 + spriteHalfHeight;
            float bottom = cam.transform.position.y - camHeight / 2 - spriteHalfHeight;

            // Dibuja una l�nea para cada borde
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(left, bottom, 0), new Vector3(left, top, 0));
            Gizmos.DrawLine(new Vector3(right, bottom, 0), new Vector3(right, top, 0));
            Gizmos.DrawLine(new Vector3(left, top, 0), new Vector3(right, top, 0));
            Gizmos.DrawLine(new Vector3(left, bottom, 0), new Vector3(right, bottom, 0));
        }
    }
}

