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
    
    public ProblemCSV(String problemFileName, String restrictionsFileName)
    {
        this.problemFileName = problemFileName;
        
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
        CsvReader restrictionsReader = new CsvReader(restrictionsFileName, false);
        return new ProblemRestrictions();
    }
    
    public boolean CheckForDataCorectness()
    {
        return true;
    }
    
}
