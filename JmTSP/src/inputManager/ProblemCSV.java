package inputManager;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;
import BasicTypes.*;

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
    
    static final int columnsInProblemFile = 3;
    
    public ProblemCSV(String problemFileName, String restrictionsFileName, String distanceFileName) {
        this.problemFileName      = problemFileName;
        this.restrictionsFileName = restrictionsFileName;
        this.distanceFileName     = distanceFileName;
        problem = new Problem();
        distances = new int[1][];
    }
    
    public Problem CreateProblem() {
        if(CreateProblemBase()){
            problem.SetRestrictions(GetProblemRestrictions());
            problem.SetDistances(GetDistances());
            
            if(IsCreatedCorrectly()) {
                return problem;
            }
        }
        return new Problem();
    }
    
    public boolean IsCreatedCorrectly() {
        return true;
    }
    
    
    private Problem GetProblem() {
        CsvReader problemReader = new CsvReader(problemFileName);
        
        return new Problem();
    }
    
    private int[][] GetDistances() {
        CsvReader distanceReader = new CsvReader(distanceFileName);
        distanceReader.NoHeader();
        
        if(distanceReader.FileExists()) {
            distanceReader.ReadData();
            distances = distanceReader.GetData();
        } else {
            CountDistances();
        }
        
        return distances;
    }
    
    private boolean CreateProblemBase(){
        
        CsvReader problemReader = new CsvReader(problemFileName);
        if (problemReader.FileExists()) {
            problemReader.ReadData();
            
            int[][] xyd = problemReader.GetData();
            if(xyd[0].length == columnsInProblemFile)
            {
                problem.SetPoints(xyd);
                return true;
            }else{
                return false;
            }
        }
        return false;
    }
    
    private ProblemRestrictions GetProblemRestrictions() {
        ProblemRestrictions restrictions = new ProblemRestrictions();
        
        CsvReader restReader = new CsvReader(restrictionsFileName);
        restReader.FirstColumnIsHeader();
        if (restReader.FileExists()) {
            restReader.ReadData();
            
            restrictions.maxCycleCargo  = restReader.GetValue("CycleCargo", 0);
            restrictions.minCycleCargo  = restReader.GetValue("CycleCargo", 1);
            restrictions.maxCycleSize   = restReader.GetValue("CycleSize", 0);
            restrictions.minCycleSize   = restReader.GetValue("CycleSize", 1);
            restrictions.maxCycleLength = restReader.GetValue("CycleLength", 0);
            restrictions.maxCycleLength = restReader.GetValue("CycleLength", 1);
            
            restrictions.maxPointCoordinate = restReader.GetValue("PointCoordinate", 0);
            restrictions.maxPointDemand     = restReader.GetValue("PointDemand", 0);
        }
        return restrictions;
    }
    
    private void CountDistances(){
        Point[] points = problem.GetPoints();
        for(int i = 0; i < points.length; ++i) {
            for(int j = 0; j < points.length; ++j){
                distances[i][j] = (int)Math.sqrt
                                 (Math.pow(points[i].GetX() - points[j].GetX(), 2.0) +
                                  Math.pow(points[i].GetY() - points[j].GetY(), 2.0));
            }
        }
    }
}
