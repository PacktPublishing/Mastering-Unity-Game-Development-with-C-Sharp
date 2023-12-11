using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    [RequireComponent(typeof(CommandProcessor))]
    public class Entity : MonoBehaviour, IEntity
    {
        CommandProcessor _commandProcessor;
        float _horizontalInput;
        float _verticalInput;
        Vector3 _direction;

        // Start is called before the first frame update
        void Start()
        {
            _commandProcessor = GetComponent<CommandProcessor>();
        }

        // Update is called once per frame
        void Update()
        {
            //Get Input
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            //construct direction
            _direction = new Vector3(_horizontalInput, _verticalInput, 0);


            //Make a capsulated command
            if (_direction != Vector3.zero)
                _commandProcessor.ExcuteCommand(new MoveCommand(this, _direction));

            if (Input.GetKeyDown(KeyCode.Space))
                _commandProcessor.ExcuteCommand(new JumpCommand(this));

            if (Input.GetKeyDown(KeyCode.Q))
                _commandProcessor.UndoCommand();

            if (Input.GetKeyDown(KeyCode.R))
                _commandProcessor.RedoCommand();
        }
    }
}