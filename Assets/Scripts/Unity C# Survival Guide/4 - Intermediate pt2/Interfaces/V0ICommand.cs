using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface V0ICommand
{
    void Execute();
    void Undo();
}
