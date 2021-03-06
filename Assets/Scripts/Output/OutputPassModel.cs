﻿using System.Collections;
using System.Collections.Generic;

public class OutputPassModel{
    public int PassID { get; set; }
    public bool IfShipFired { get; set; }
    public float TimePlayerShotInSeconds { get; set; }
    public float ProjAsteroidMinDistance { get; set; }
    public bool IfAsteroidWasHit { get; set; }

    public override string ToString()
    {
        int ifShipFired = IfShipFired ? 1 : 0;
        int ifAsteroidWasHit = IfAsteroidWasHit ? 1 : 0;
        return "(" + PassID + "; " + ifShipFired + "; " + ifAsteroidWasHit + "; " + TimePlayerShotInSeconds + "; " + ProjAsteroidMinDistance + ")";
    }
}
