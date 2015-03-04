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
    
    Problem problem;
    
    public ProblemCSV(String problemFileName, String restrictionsFileName, String distanceFileName)
    {
        this.problemFileName      = problemFileName;
        this.restrictionsFileName = restrictionsFileName;
        this.distanceFileName     = distanceFileName;
        problem = new Problem();
    }
    
    public Problem CreateProblem()
    {
        ProblemRestrictions restrictions = GetProblemRestrictions();
        
        
        return new Problem();
    }
    
    private Problem GetProblem() {
        CsvReader problemReader = new CsvReader(problemFileName, false);
        
        return new Problem();
    }
    
    private Number[][] GetDistance(Problem problem) {
        Number[][] distances = new Number[1][];
        CsvReader distanceReader = new CsvReader(distanceFileName, false);
        
        if(distanceReader.FileExists()) {
            distanceReader.ReadData();
            distances = distanceReader.GetData();
        } else {
            
        }
        
        return distances;
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
