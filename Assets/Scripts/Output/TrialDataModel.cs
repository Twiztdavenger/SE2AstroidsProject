using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Output
{

    class TrialDataModel : ScriptableObject
    {
        /// <summary>
        /// DataOutput Specs for Trial Data:
        /// -Trial ID
        /// -Participant ID
        /// -If round was practice or real round
        /// -Total number of passes allowed
        /// -Speed of loop
        /// -Delay Time (if experimenter decides to choose one
        /// -Whether or not Experimenter wants to display score
        /// -Relevant Ship Data
        /// -Relevant Asteroid Data
        /// 
        /// TODO: Figure out where to instantiate trial object 
        ///     into the scene and grab data from it (Do we control trial object HERE or in trialController?)
        /// </summary>
        List<PassDataModel> passList = new List<PassDataModel>();


        public int TrialID { get; set; }
        public int ParticipantID { get; set; }
        public string RoundStatus { get; set; }
        public int TotalNumPasses { get; set; }

        public float SpeedOfLoop { get; set; }
        public float DelayTime { get; set; }

        public bool DisplayScore { get; set; }

        // Ship Data
        public Vector3 ShipSpawn { get; set; }
        public bool ShipMove { get; set; }
        public bool ShipRotate { get; set; }
        public float ShipMoveSpeed { get; set; }
        public float ShipRotateSpeed { get; set; }

        // Asteroid Data
        public Vector3 AsteroidSpawn { get; set; }
        public float AsteroidMovementX { get; set; }
        public float AsteroidMovementY { get; set; }
        public float AsteroidRotation { get; set; }

        float totalTrialTime;
        bool trialDone = false;



    }
}
