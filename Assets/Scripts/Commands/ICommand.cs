using System.Collections;

namespace RPG_Project
{
    public interface ICommand
    {
        void Execute();
        IEnumerator ExecuteCo();
    }
}