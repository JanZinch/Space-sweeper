namespace CodeBase.ApplicationLibrary.Application
{
    public static class ApplicationUtils
    {
        private static bool GameInitializeState = false;


        public static void SetInitializedGameSession() => GameInitializeState = true;
        public static void SetNotInitializedGameSession() => GameInitializeState = false;
        public static bool GetGameInitializeState() => GameInitializeState;
    }
}