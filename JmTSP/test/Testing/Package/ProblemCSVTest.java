/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
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
    public void testMaxCycleSize()
    {
       problemCreator = new ProblemCSV( "test/testFiles/simpleProblem.csv",
                                        "test/testFiles/simpleRestrictions.csv",
                                        "test/testFiles/simpleProblemDistances.csv");
       
       problem = problemCreator.CreateProblem();
    }
    
}
