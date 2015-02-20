
package brute;
import java.util.ArrayList;
import jmtsp.*;

/**
 *
 * @author lbiedak
 */
public class SimpleSolver
implements Solver{
    
    ArrayList<Cycle> cycles;
    Problem problem;
    CyclesCreator creator;
    CyclesConnector connector;
    
    /**
     * MaxProblemSize informs how big problem class can Solve
     */
    public int MaxProblemSize(Problem problem)
    {
        return 16; //TODO: compute something
    }
    
    /**
     * LoadProblem informs loads problem and inform if solving
 will be possible
     */
    public boolean LoadProblem(Problem problem)
    {
        if(problem.size <= MaxProblemSize(problem))
        {
            this.problem = problem;
            
            return true;
        }
        return false;
    }
    
    /**
     * Solve() returns solution of a problem
     */
    public Solution Solve()
    {
        // 1. CreateCycles() returns List of Created cycles
        // 1.a CycleAnalyzer - divide into basic/rest cycles
        // 2. ConnectCycles() returns List of Connected Cycles
        // 3. return best Solution
        return new Solution();
    }
    
    /**
     * Solve(howMany) returns few bests solutions of a problem
     */
    public ArrayList<Solution> Solve(int howMany)
    {
        // 1. CreateCycles() returns List of Created cycles
        // 2. ConnectCycles() returns possible Solutions
        // 3. return few bests solutions
        return new ArrayList<Solution>();
    }
    
    public SimpleSolver()
    {
        cycles = new ArrayList<Cycle>();
        problem = new Problem();
    }
    
    private void RunSolver()
    {
        
    }
}
