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
    
    public ProblemRestrictions()
    {
        maxCycleSize = 5;
        minCycleSize = 1;
        
        maxCycleLength = 5000;
        minCycleLength = 0;
        
        maxCycleCargo = 1000;
        minCycleCargo = 500;
    }
    
    public ProblemRestrictions(int maxCS, int maxCL, int maxCC)
    {
        this();
        maxCycleSize = maxCS;
        maxCycleLength = maxCL;
        maxCycleCargo = maxCC;
    }
    
    public void SetMaxCycleSize()
    {
        
    }
}
