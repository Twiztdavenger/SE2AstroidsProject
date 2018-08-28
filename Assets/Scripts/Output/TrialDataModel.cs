using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Output
{

    public class TrialDataModel 
    {
        /// <summary>
        /// DataOutput Specs for Trial Data:
        /// -Trial ID
        /// -Participant ID
        /// -If round was practice or real round
        /// -Total number of passes allowed
        /// -Speed of loop
        /// -Delay Time (if experimenter decides to choose one)
        /// -Whether or not Experimenter wants to display score
        /// -Relevant Ship Data
        /// -Relevant Asteroid Data
        /// 
        /// TODO: Figure out where to instantiate trial object 
        ///     into the scene and grab data from it (Do we control trial object HERE or in trialController?)
        /// </summary>
        public Queue<PassDataModel> passList = new Queue<PassDataModel>();

        public string TrialName { get; set; }
        public int ParticipantID { get; set; }
        public string RoundStatus { get; set; }
        public int TotalNumPasses { get; set; }

        public float SpeedEachPass { get; set; }
        public float SpawnDelayTime { get; set; }

        public bool DisplayScore { get; set; }

        // Ship Data
        public float ShipSpawnX { get; set; }
        public float ShipSpawnY { get; set; }
        public bool ShipMove { get; set; }
        public bool ShipRotate { get; set; }
        public float ShipMoveSpeed { get; set; }
        public float ShipRotateSpeed { get; set; }

        // Asteroid Data
        public float AsteroidSpawnX { get; set; }
        public float AsteroidSpawnY { get; set; }
        public float AsteroidMovementX { get; set; }
        public float AsteroidMovementY { get; set; }
        public float AsteroidRotation { get; set; }

        float totalTrialTime;
        bool trialDone = false;




        public void addPass(int num, bool wasFired, bool hit, float projFireTime, float totalPassTime, float minProjX, float minProjY, float minAstX, float minAstY)
        {
            PassDataModel tempPassData = new PassDataModel();

            tempPassData.passID = num;
            tempPassData.wasFired = wasFired;
            tempPassData.hit = hit;
            tempPassData.projFireTime = projFireTime;
            tempPassData.totalPassTime = totalPassTime;

            tempPassData.minProjCoordX = minProjX;
            tempPassData.minProjCoordY = minProjY;

            tempPassData.minAsCoordX = minAstX;
            tempPassData.minAsCoordY = minAstY;

            passList.Enqueue(tempPassData);
        }

        public string returnPassData()
        {
            string output = "";
            while(passList.Count != 0)
            {
                PassDataModel temp = passList.Dequeue();
                
                output += "(" + temp.passID + "; ";
                output += temp.wasFired + "; ";
                output += temp.hit + "; ";
                output += temp.projFireTime + "; ";
                output += temp.projFireTime + "; ";
                output += temp.totalPassTime + "; ";
                output += temp.minProjCoordX + "; ";
                output += temp.minProjCoordY + "; ";
                output += temp.minAsCoordX + "; ";
                output += temp.minAsCoordY + ")";
            }
            return output;
        }
    }
}
