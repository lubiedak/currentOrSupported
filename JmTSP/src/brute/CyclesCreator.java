package brute;
import jmtsp.*;
import java.util.ArrayList;
import java.lang.Math;

/**
 *
 * @author lbiedak
 */
public class CyclesCreator {
    Problem problem;
    ArrayList<Cycle> cycles;
    PermutationGenerator permGen;
    
    int maxCycleSize;
    
    public CyclesCreator(Problem problem)
    {
        this.problem = problem;
        cycles = new ArrayList<Cycle>();
        maxCycleSize = Math.min( MethodLimits.maxCycleSize, problem.GetMaxCycleSize() );
        permGen = new PermutationGenerator( maxCycleSize );
    }
    
    public int GetMaxCycleSize()
    {
        return maxCycleSize;
    }
    
    public ArrayList<Cycle> Create()
    {
        return cycles;
    }
    
    private int CountN()
    {
        return (int)( Math.pow(2.0, problem.size) - 1 -
                      Math.pow(2.0, problem.size - maxCycleSize));
    }
    
}
