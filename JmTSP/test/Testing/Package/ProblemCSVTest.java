
package Testing.Package;

import inputManager.ProblemCSV;
import jmtsp.Problem;
import org.junit.Test;

/**
 *
 * @author lbiedak
 */
public class ProblemCSVTest {
    private ProblemCSV problemCreator;
    private Problem problem;
    
    @Test
    public void testMaxCycleSize() {
        /**
         * What was the point here?
         */
        problemCreator = new ProblemCSV( "test/testFiles/simpleProblem.csv",
                                        "test/testFiles/simpleRestrictions.csv",
                                        "test/testFiles/simpleProblemDistances.csv");
        
        problem = problemCreator.CreateProblem();
    }
    
}
