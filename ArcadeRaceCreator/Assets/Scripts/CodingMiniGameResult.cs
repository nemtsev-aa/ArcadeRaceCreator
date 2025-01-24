using System;
using System.Collections.Generic;

[Serializable]
public class CodingMiniGameResult {
    public CodingMiniGameResult(List<EquationConfig> equation, bool status) {
        Equation = equation;
        Status = status;
    }

    public List<EquationConfig> Equation { get; private set; }
    public bool Status { get; private set; }
}
