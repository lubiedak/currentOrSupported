
package brute;
import BasicTypes.*;
import java.util.ArrayList;
import jmtsp.*;

/**
 *
 * @author lbiedak
 */
public class BruteSolver
implements Solver{
    
    /**
     * MaxProblemSize informs how big problem class can Solve
     */
    public int MaxProblemSize(Problem problem) {
        return 0;
    }
    
    /**
     * LoadProblem informs loads problem and inform if solving will be possible
     */
    public boolean LoadProblem(Problem problem) {
        if(problem.GetMaxCycleSize() > MethodLimits.maxCycleSize) {
            //warn user
        }
        return true;
    }
    
    /**
     * Solve() returns solution of a problem
     */
    public Solution Solve() {
        return new Solution();
    }
    
    /**
     * Solve(howMany) returns few bests solutions of a problem
     */
    public ArrayList<Solution> Solve(int howMany) {
        return new ArrayList<Solution>();
    }
    
}
