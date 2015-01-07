using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using GameModel;
using System.Windows.Forms;

namespace AI4PowerGrid_gui
{
    public enum phase
    {
        UPDATE_ORDER_TRACK = 1,
        AUCTION,
        RESOURCE_BUYING,
        BUYING_NEW_HOUSES,
        SELLING_ELECTRICITY,
        BUREAUCRACY,
        N_OF_PHASES
    }

    public static class GameFlow
    {
        public static int round;
        public static int step;
        public static phase active_phase;

        static GameFlow()
        {
            active_phase = phase.AUCTION;
            round = 0;
            step = 0;
        }

        public static  void LetsPlay()
        {
        
        }

        public static bool IsGameEnded()
        {
            return false;
        }

        public static void NextPhase()
        {
            if (active_phase == phase.BUREAUCRACY)
            {
                active_phase = phase.UPDATE_ORDER_TRACK;
                round++;
            }
            else
            {
                active_phase++;
            }
        }
    }
}
