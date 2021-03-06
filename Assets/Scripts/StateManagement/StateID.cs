namespace RPG_Project
{
    public enum StateID
    {
        Empty = 0,

        ControllerMove = 1,
        ControllerRun = 2,
        ControllerFall = 3,
        ControllerRecover = 4,
        ControllerAction = 5,
        ControllerStagger = 6,
        ControllerDeath = 7,
        ControllerStrafe = 8,
        ControllerGuard = 9,
        ControllerMoveThirdPerson = 10,
        ControllerMoveTopDown = 11,
        ControllerMoveSideScroll = 12,

        EnemyIdle = 21,
        EnemyChase = 22,
        EnemyAttack = 23,
    }
}