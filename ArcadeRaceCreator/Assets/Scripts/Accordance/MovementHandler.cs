using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementHandler : IDisposable {
    public event Action<MultipliersCompositionView> CompositionViewSelected;
    public event Action<Vector2> Dragged;
    public event Action<MultipliersResultView> ResultViewSelected;

    private IInput _input;
    private bool _isDragging;

    public MovementHandler(IInput input) {
        _input = input;

        Debug.Log(input.GetType());

        _input.ClickDown += OnClickDown;
        _input.ClickUp += ClickUp;
        _input.Drag += OnDrag;
    }

    private void OnClickDown(Vector3 position) {

        //// Получаем позицию курсора в экранных координатах
        //PointerEventData pointerData = new PointerEventData(EventSystem.current);
        //pointerData.position = Input.mousePosition;

        //// Создаем список результатов Raycast'а
        //List<RaycastResult> results = new List<RaycastResult>();

        //// Выполняем Raycast через EventSystem
        //EventSystem.current.RaycastAll(pointerData, results);

        //// Обрабатываем результаты Raycast'а
        //foreach (RaycastResult result in results)
        //{
        //    Debug.Log("Hit UI Element: " + result.gameObject.name);
        //    // Здесь можно добавить дополнительную логику для взаимодействия с найденным элементом
        //}
        
        RaycastHit2D hit = Physics2D.Raycast(GetWorldPoint(position), -Vector2.down);

        if (hit.collider != null && hit.collider.TryGetComponent(out MultipliersCompositionView compositionView)) {
            _isDragging = true;
            CompositionViewSelected?.Invoke(compositionView);
        }
    }

    private void OnDrag(Vector3 position) {
        if (_isDragging) 
            Dragged?.Invoke(GetWorldPoint(position));
    }

    private void ClickUp(Vector3 position) {
       
        _isDragging = false;

        RaycastHit2D hit = Physics2D.Raycast(GetWorldPoint(position), -Vector2.up);
        
        if (hit.collider != null && hit.collider.TryGetComponent(out MultipliersResultView resultView)) {
            _isDragging = false;
            ResultViewSelected?.Invoke(resultView);
        } else {
            ResultViewSelected?.Invoke(null);
        }
    }

    private Vector3 GetWorldPoint(Vector3 position) {
        return Camera.main.ScreenToWorldPoint(position);
    }

    public void Dispose() {
        _input.ClickDown -= OnClickDown;
        _input.ClickUp -= ClickUp;
        _input.Drag -= OnDrag;
    }
}
