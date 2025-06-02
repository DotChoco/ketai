using System;
using System.Collections.Generic;
namespace RDE.Input;

public static class Input {
    public static List<(Func<bool>, Key)> Buttons = new();
    
    public static bool SetInputAction(Func<string> Action, Key Btn) {
        return false;
    }

    public static bool InputExec(Key Btn){
        return false;
    }

    public static bool ChangeInputAction(){
        return false;
    }


}

