using System;
using System.Drawing;

namespace ConnectFour
{
    public class CoinAnimationEventArgs : EventArgs
    {
        private readonly Point r_StartLoaction;
        private readonly int r_BotoomLimit;
        private readonly CoinEventArgs r_CoinArgs;
        private readonly EState r_State;
        private EventHandler m_EventHandlerMethod;

        public CoinAnimationEventArgs(Point i_StartLocation, int i_BotoomLimit, CoinEventArgs i_CoinArgs, EState i_State)
        {
            r_StartLoaction = i_StartLocation;
            r_BotoomLimit = i_BotoomLimit;
            r_CoinArgs = i_CoinArgs;
            r_State = i_State;
        }

        public EventHandler EventHandlerMethod
        {
            get { return m_EventHandlerMethod; }
            set { m_EventHandlerMethod = value; }
        }

        public EState State
        {
            get { return r_State; }
        }

        public CoinEventArgs CoinArgs
        {
            get { return r_CoinArgs; }
        }

        public Point StartLoaction
        {
            get { return r_StartLoaction; }
        }

        public int BotoomLimit
        {
            get { return r_BotoomLimit; }
        }
    }
}
