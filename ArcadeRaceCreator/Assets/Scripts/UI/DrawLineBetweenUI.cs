using UnityEngine;

public class DrawLineBetweenUI : MonoBehaviour {
    public RectTransform rectTransform1; // Первый UI объект
    public RectTransform rectTransform2; // Второй UI объект

    public LineRenderer lineRenderer; // Компонент для отрисовки линии

    void Start() {
        if (lineRenderer == null) {
            GameObject lineObject = new GameObject("Line");
            lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.useWorldSpace = true;
        }
    }

    [ContextMenu(nameof(DrawLine))]
    public void DrawLine() {
        // Очищаем предыдущую линию
        lineRenderer.positionCount = 0;

        // Получаем мировые координаты центров UI объектов
        Vector3 worldPosition1 = GetWorldPosition(rectTransform1);
        Vector3 worldPosition2 = GetWorldPosition(rectTransform2);

        // Устанавливаем количество точек линии
        lineRenderer.positionCount = 2;

        // Устанавливаем позиции линии
        lineRenderer.SetPosition(0, worldPosition1);
        lineRenderer.SetPosition(1, worldPosition2);
    }

    Vector3 GetWorldPosition(RectTransform rectTransform) {
        // Преобразуем локальные координаты центра UI объекта в мировые координаты
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return corners[0]; // Верхний левый угол
        // Вы можете выбрать другой угол или среднюю точку по вашему усмотрению
        // Например, средняя точка:
        // return (corners[0] + corners[2]) / 2;
    }
}
