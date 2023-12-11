using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{


    public class CommandProcessor : MonoBehaviour
    {
        private List<Command> commands = new List<Command>();
        private int _currentCommandIndex;

        public void ExcuteCommand(Command command)
        {
            commands.Add(command);
            command.Excute();
            _currentCommandIndex = commands.Count - 1;
        }

        public void UndoCommand()
        {
            if (_currentCommandIndex < 0)
                return;

            commands[_currentCommandIndex].Undo();
            commands.RemoveAt(_currentCommandIndex);
            _currentCommandIndex--;
        }

        public void RedoCommand()
        {
            ExcuteCommand(commands[_currentCommandIndex]);
        }
    }
}