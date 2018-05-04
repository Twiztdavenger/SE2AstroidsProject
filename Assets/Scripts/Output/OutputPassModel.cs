using System.Collections;
using System.Collections.Generic;

public class OutputPassModel{
    public int passNum;
    public bool wasFired;
    public float timeProjFired; //relative to beginning of pass
    public int timeOfPass; //Do we really need this?
    public float minDistance;

    public OutputPassModel(int passNum, bool wasFired, float timeProjFired, int timeOfPass, float minDistance)
    {
        this.passNum = passNum;
        this.wasFired = wasFired;
        this.timeProjFired = timeProjFired;
        this.timeOfPass = timeOfPass;
        this.minDistance = minDistance;
    }
}
