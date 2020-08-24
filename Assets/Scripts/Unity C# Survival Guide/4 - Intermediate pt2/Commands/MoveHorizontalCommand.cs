using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontalCommand : ICommand
{
    private Transform _player;
    private float _speed, _input;

    public MoveHorizontalCommand(Transform player, float speed, float input)
    {
        this._player = player;
        this._speed = speed;
        this._input = input;
    }

    public void Execute()
    {
        _player.Translate(new Vector3(_input, 0, 0) * _speed * Time.deltaTime);
    }

    public void Undo()
    {
        _player.Translate(new Vector3(_input, 0, 0) * -_speed * Time.deltaTime);
    }
}
