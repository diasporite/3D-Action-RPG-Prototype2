using System.Collections;

namespace RPG_Project
{
    public interface ICommand
    {
        void Execute();
        void Undo();

        IEnumerator ExecuteCo();
        IEnumerator UndoCo();
    }
}