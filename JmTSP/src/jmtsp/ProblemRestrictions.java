package jmtsp;

/**
 * Simple structure with restrictions for possible solutions
 * @author lbiedak
 */
public class ProblemRestrictions {
    
    public int maxCycleSize;
    public int minCycleSize;
    
    public int maxCycleLength;
    public int minCycleLength;
    
    public int maxCycleCargo;
    public int minCycleCargo;
    
    public int maxPointCoordinate;
    public int maxPointDemand;
    
    public ProblemRestrictions() {
        maxCycleSize = 5;
        minCycleSize = 1;
        
        maxCycleLength = 5000;
        minCycleLength = 0;
        
        maxCycleCargo = 1000;
        minCycleCargo = 500;
        
        maxPointCoordinate = 1000;
        maxPointDemand = maxCycleCargo;
    }
    
    public ProblemRestrictions(int maxCS, int maxCL, int maxCC) {
        this();
        maxCycleSize = maxCS;
        maxCycleLength = maxCL;
        maxCycleCargo = maxCC;
        
    }
    
    public void SetMaxCycleSize() {
        
    }
    
    @Override
    public String toString(){
        String description = "Problem restrictions:\n";
        
        description += "maxCycleSize:   " + maxCycleSize + "\n";
        description += "minCycleSize:   " + minCycleSize + "\n";
        description += "maxCycleLength: " + maxCycleLength + "\n";
        description += "minCycleLength: " + minCycleLength + "\n";
        description += "maxCycleCargo:  " + maxCycleCargo + "\n";
        description += "minCycleCargo:  " + minCycleCargo + "\n";
        description += "maxPointCoord:  " + maxPointCoordinate + "\n";
        description += "maxPointDemand: " + maxPointDemand + "\n";
        return description;
    }
}
