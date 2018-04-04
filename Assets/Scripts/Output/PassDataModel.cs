using System.Collections.Generic;

namespace Assets.Scripts.Output 
{

    /// <summary>
    /// Dr. Levinthal's Specs for Data Output for Pass Data:
    /// -Pass Number
    /// -Whether the Projectile was fired
    /// -Whether the Projectile hit or missed
    /// -The time the Projectile was fired
    /// -The total time of the pass
    /// -Positions of both the projectile and the asteroid when 
    ///     distance between the two is at a minimum 
    /// </summary>
    class PassDataModel 
    {

        public int passID { get; set; }

        public bool projectileFired { get; set; }
        public bool hit { get; set; }
        public float projFireTime { get; set; }
        public float totalPassTime { get; set; }

        // Position of proj when closest to asteroid
        public float minProjCoordX { get; set; }
        public float minProjCoordY { get; set; }

        // Position of asteroid when closest to proj
        public float minAsCoordX { get; set; }
        public float minAsCoordY { get; set; }


        //TODO: Calculate angle of projectile fired



    }
}
