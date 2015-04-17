
package brute;

import BasicTypes.*;
import jmtsp.*;

/**
 *
 * @author lbiedak
 */
public class BruteTSPSolver {
    Problem problem;
    
    PermutationGenerator permGen;
    
    public BruteTSPSolver(Problem problem){
        this.problem = problem;
        permGen = new PermutationGenerator(problem.GetMaxCycleSize());
    }
    
    public Cycle CreateCycle(Integer[] selectedPoints){
        
        return new Cycle();
    }
    
    private void FindShortestPerm(){
        int length = 0;
    }
}
