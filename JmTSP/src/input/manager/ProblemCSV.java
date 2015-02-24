package input.manager;

import jmtsp.Problem;
import java.io.FileNotFoundException;
import java.io.IOException;

/**
 *
 * @author lbiedak
 */
public class ProblemCSV 
implements ProblemCreator {
    String fileName;
    
    public ProblemCSV(String fileName)
    {
        this.fileName = fileName;
    }
    
    public Problem CreateProblem()
    {
        
        
        return new Problem();
    }
    
    public boolean CheckForDataCorectness()
    {
        return true;
    }
    
}
