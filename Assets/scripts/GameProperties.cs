using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts
{
    class GameProperties
    {

        // world setting
        public static int WORLD_SIZE = 50;
        public static float WORLD_UPDATE_DELAY = 3.5f;
        public static bool COMPLEX_INFECTION = false;

        // healer settings
        public static int HEALER_CONSTRUCTION_PICKUP_COST = 5;

        // infection settings
        public static float INFECTION_MIN = 0.0f;
        public static float INFECTION_POINT = 0.8f;
        public static float INFECTION_MAX = 1.0f;
        public static int INFECTION_RANGE = 4;

        // player settings
        // health
        public static int PLAYER_MAX_HEALTH = 100;
        public static int PLAYER_STARTING_HEALTH = 100;
        // stamina
        public static int PLAYER_STAMINA = 500;
        public static int PLAYER_STAMINA_DEC_RATE_FRAME = 1;
        public static int PLAYER_STAMINA_RECOVERY = 20;
        public static int PLAYER_STAMINA_RECOVERY_RATE_SECONDS = 1;
        // pickups
        public static int PLAYER_STARTING_PICKUPS = 50; //TODO: debug value, for testing only
    }
}
