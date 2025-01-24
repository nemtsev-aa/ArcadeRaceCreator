using System;
using UnityEngine.EventSystems;

public class MultipliersCompositionView : AccordanceCompanent, IPointerClickHandler {
    public event Action<MultipliersCompositionView> CompositionViewSelected;

    public string Description { get; private set; }
    public string Name { get; private set; }
    
    protected override string GetLabelValueText() {
        var compositionConfig = (MultipliersCompositionViewConfig)Config;
        Description = compositionConfig.Data.Description;
        Name = compositionConfig.Data.Name;

        return $"{Description}";
    }

    public void OnPointerClick(PointerEventData eventData) {
        CompositionViewSelected?.Invoke(this);
    }
}

