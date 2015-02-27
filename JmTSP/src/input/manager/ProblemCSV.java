package input.manager;

import jmtsp.Problem;
import java.io.FileNotFoundException;
import java.io.IOException;
import jmtsp.ProblemRestrictions;

/**
 *
 * @author lbiedak
 */
public class ProblemCSV 
implements ProblemCreator {
    String problemFileName;
    String restrictionsFileName;
    String distanceFileName;
    
    public ProblemCSV(String problemFileName, String restrictionsFileName, String distanceFileName)
    {
        this.problemFileName      = problemFileName;
        this.restrictionsFileName = restrictionsFileName;
        this.distanceFileName     = distanceFileName;
    }
    
    public Problem CreateProblem()
    {
        
        
        
        return new Problem();
    }
    
    private Problem GetProblem()
    {
        CsvReader problemReader = new CsvReader(problemFileName, false);
        
        return new Problem();
    }
    
    private ProblemRestrictions GetProblemRestrictions()
    {
        ProblemRestrictions restrictions = new ProblemRestrictions();
        
        CsvReader restReader = new CsvReader(restrictionsFileName, false);
        if (restReader.FileExists())
        {
            restReader.ReadData();
            
            restrictions.maxCycleCargo = restReader.GetIntValue("CycleCargo", 0);
            restrictions.minCycleCargo = restReader.GetIntValue("CycleCargo", 1);
            restrictions.maxCycleSize = restReader.GetIntValue("CycleSize", 0);
            restrictions.minCycleSize = restReader.GetIntValue("CycleCargo", 1);
            restrictions.maxCycleLength = restReader.GetIntValue("CycleLength", 0);
            restrictions.maxCycleLength = restReader.GetIntValue("CycleLength", 1);
            
            restrictions.maxPointCoordinate = restReader.GetIntValue("PointCoordinate", 0);
            restrictions.maxPointDemand = restReader.GetIntValue("PointDemand", 0);
        }
        return restrictions;
    }
    
    public boolean CheckForDataCorectness()
    {
        return true;
    }
    
}
