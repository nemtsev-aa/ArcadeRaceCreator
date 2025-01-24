public class MultipliersCompositionViewConfig : UICompanentConfig {
 
    public MultipliersCompositionViewConfig(EquationConfig data) {
        Data = data;
    }

    public EquationConfig Data { get; private set; }

    public override void OnValidate() {

    }
}
