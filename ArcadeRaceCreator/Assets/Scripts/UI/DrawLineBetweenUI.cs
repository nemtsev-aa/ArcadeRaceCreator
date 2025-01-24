using UnityEngine;

public class DrawLineBetweenUI : MonoBehaviour {
    public RectTransform rectTransform1; // ������ UI ������
    public RectTransform rectTransform2; // ������ UI ������

    public LineRenderer lineRenderer; // ��������� ��� ��������� �����

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
        // ������� ���������� �����
        lineRenderer.positionCount = 0;

        // �������� ������� ���������� ������� UI ��������
        Vector3 worldPosition1 = GetWorldPosition(rectTransform1);
        Vector3 worldPosition2 = GetWorldPosition(rectTransform2);

        // ������������� ���������� ����� �����
        lineRenderer.positionCount = 2;

        // ������������� ������� �����
        lineRenderer.SetPosition(0, worldPosition1);
        lineRenderer.SetPosition(1, worldPosition2);
    }

    Vector3 GetWorldPosition(RectTransform rectTransform) {
        // ����������� ��������� ���������� ������ UI ������� � ������� ����������
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return corners[0]; // ������� ����� ����
        // �� ������ ������� ������ ���� ��� ������� ����� �� ������ ����������
        // ��������, ������� �����:
        // return (corners[0] + corners[2]) / 2;
    }
}
