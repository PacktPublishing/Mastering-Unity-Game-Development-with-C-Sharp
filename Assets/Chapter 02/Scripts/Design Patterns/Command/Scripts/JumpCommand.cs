using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CommandPattern
{
    public class JumpCommand : Command
    {
        
        public JumpCommand(IEntity entity) : base(entity)
        {
        }

        public override void Excute()
        {
            Debug.Log("Excute jump Command ");
            _entity.transform.position +=  Vector3.up;
        }

        public override void Undo()
        {
            Debug.Log("Undo jump Command ");
            _entity.transform.position -= Vector3.up;
        }
    }
}