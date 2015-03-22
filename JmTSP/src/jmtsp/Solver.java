package jmtsp;
import java.util.ArrayList;

/**
 *
 * @author lbiedak
 */
public interface Solver {
    /**
     * MaxProblemSize informs how big problem class can Solve
     */
    int MaxProblemSize(Problem problem);
    
    /**
     * LoadProblem informs loads problem and inform if solving 
     * will be possible
     */
    boolean LoadProblem(Problem problem);
    
    /**
     * Solve() returns solution of a problem
     */
    Solution Solve();
    
    /**
     * Solve(howMany) returns few bests solutions of a problem
     */
    ArrayList<Solution> Solve(int howMany);
}
