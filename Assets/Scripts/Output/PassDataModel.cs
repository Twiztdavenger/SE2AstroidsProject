using System.Collections.Generic;
using UnityEngine;

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
    class PassDataModel : MonoBehaviour
    {

        public int passID { get; set; }

        bool projectileFired { get; set; }
        bool hit { get; set; }
        float projFireTime { get; set; }
        float totalPassTime { get; set; }

        // Position of proj when closest to asteroid
        Vector3 minProjCoord { get; set; }

        // Position of asteroid when closest to proj
         Vector3 minAsCoord { get; set; }

        //TODO: Calculate angle of projectile fired



    }
}
