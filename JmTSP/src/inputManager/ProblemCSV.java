package inputManager;

import jmtsp.Problem;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;
import jmtsp.Point;
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
    int[][] distances;
    
    
    public ProblemCSV(String problemFileName, String restrictionsFileName, String distanceFileName) {
        this.problemFileName      = problemFileName;
        this.restrictionsFileName = restrictionsFileName;
        this.distanceFileName     = distanceFileName;
        problem = new Problem();
        distances = new int[1][];
    }
    
    public Problem CreateProblem() {
        ProblemRestrictions restrictions = GetProblemRestrictions();
        
        
        return new Problem();
    }
    
    public boolean CheckForDataCorectness() {
        return true;
    }
    
    private Problem GetProblem() {
        CsvReader problemReader = new CsvReader(problemFileName, false);
        
        return new Problem();
    }
    
    private int[][] GetDistance(Problem problem) {
        CsvReader distanceReader = new CsvReader(distanceFileName, false);
        
        if(distanceReader.FileExists()) {
            distanceReader.ReadData();
            distances = distanceReader.GetData();
        } else {
            
        }
        
        return distances;
    }
    
    private ProblemRestrictions GetProblemRestrictions() {
        ProblemRestrictions restrictions = new ProblemRestrictions();
        
        CsvReader restReader = new CsvReader(restrictionsFileName, false);
        if (restReader.FileExists()) {
            restReader.ReadData();
            
            restrictions.maxCycleCargo = restReader.GetValue("CycleCargo", 0);
            restrictions.minCycleCargo = restReader.GetValue("CycleCargo", 1);
            restrictions.maxCycleSize = restReader.GetValue("CycleSize", 0);
            restrictions.minCycleSize = restReader.GetValue("CycleCargo", 1);
            restrictions.maxCycleLength = restReader.GetValue("CycleLength", 0);
            restrictions.maxCycleLength = restReader.GetValue("CycleLength", 1);
            
            restrictions.maxPointCoordinate = restReader.GetValue("PointCoordinate", 0);
            restrictions.maxPointDemand = restReader.GetValue("PointDemand", 0);
        }
        return restrictions;
    }
    
    private void CountDistances(Problem problem){
        Point[] points = problem.GetPoints();
        for(int i = 0; i < points.length; ++i){
            for(int j = 0; j < points.length; ++j){
                distances[i][j] = (int)Math.sqrt
                                 (Math.pow(points[i].GetX() - points[j].GetX(), 2.0) +
                                  Math.pow(points[i].GetY() - points[j].GetY(), 2.0));
            }
        }
    }
}
