public class MultipliersResultConfig : UICompanentConfig {
    public MultipliersResultConfig(string name) {
        Name = name;
    }

    public string Name { get; private set; }

    public override void OnValidate() {
        
    }
}
