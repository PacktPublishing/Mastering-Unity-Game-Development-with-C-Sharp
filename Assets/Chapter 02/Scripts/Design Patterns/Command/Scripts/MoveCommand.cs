using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{

    public class MoveCommand : Command
    {
        private Vector3 _direction;
        public MoveCommand(IEntity entity , Vector3 direction) : base(entity)
        {
            _direction = direction;
        }
        public override void Excute()
        {
            Debug.Log("Excute Move Command ");
            _entity.transform.position += _direction * 0.1f;
        }

        public override void Undo()
        {
            Debug.Log("Undo Move Command ");
            _entity.transform.position -= _direction * 0.1f;
        }
    }
}