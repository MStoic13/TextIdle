using System.Collections.Generic;

namespace TextIdle
{
    public static class Constants
    {
        public static Dictionary<Target, Drop> TargetDropMap = new Dictionary<Target, Drop>() {
            { Target.Woodcutting, Drop.Wood} 
        };
    }

    public enum Action
    {
        Start,
        End,
        See
    }

    public enum Target
    {
        Idling,
        Woodcutting,
        Bank
    }

    public enum Drop
    {
        Wood
    }
}
