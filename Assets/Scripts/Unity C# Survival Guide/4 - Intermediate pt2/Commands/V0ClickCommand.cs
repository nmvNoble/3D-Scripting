using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0ClickCommand : V0ICommand
{
    private GameObject _obj;
    private Color _color, _prevColor;
    private bool _rand;
    public V0ClickCommand(GameObject obj, Color color, bool rand = false)
    {
        this._obj = obj;
        this._color = color;
        this._rand = rand;
    }

    public void Execute()
    {
        _prevColor = _obj.GetComponent<MeshRenderer>().material.color;
        UtilityHelper.ChangeColor(_obj, _color, _rand);
    }

    public void Undo()
    {
        UtilityHelper.ChangeColor(_obj, _prevColor, true);
    }

}
