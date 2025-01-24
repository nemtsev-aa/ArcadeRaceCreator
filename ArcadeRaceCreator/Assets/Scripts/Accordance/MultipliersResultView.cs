using System;
using UnityEngine.EventSystems;

public class MultipliersResultView : AccordanceCompanent, IPointerClickHandler {
    public event Action<MultipliersResultView> ResultViewSelcted;

    public string Name { get; private set; }

    protected override string GetLabelValueText() {
        var resultConfig = (MultipliersResultConfig)Config;
        Name = resultConfig.Name;

        return $"{Name}";
    }

    public void OnPointerClick(PointerEventData eventData) {
        ResultViewSelcted?.Invoke(this);
    }
}


