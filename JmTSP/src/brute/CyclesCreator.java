package brute;
import BasicTypes.*;
import jmtsp.*;
import java.util.ArrayList;
import java.lang.Math;
import tools.BinArrayCaster;

/**
 *
 * @author lbiedak
 */
public class CyclesCreator {
    Problem problem;
    BruteTSPSolver tspSolver;
    ArrayList<Cycle> cycles;
    PermutationGenerator permGen;
    
    int maxCycleSize;
    
    public CyclesCreator(Problem problem) {
        this.problem = problem;
        tspSolver = new BruteTSPSolver(problem);
        cycles = new ArrayList<Cycle>();
        maxCycleSize = Math.min( MethodLimits.maxCycleSize, problem.GetMaxCycleSize() );
        permGen = new PermutationGenerator( maxCycleSize );
    }
    
    public int GetMaxCycleSize() {
        return maxCycleSize;
    }
    
    public ArrayList<Cycle> Create() {
        int N = CountN();
        
        int cycleSize = 0;
        int cargo = 0;
        
        /**
         * We start from 1, because we always want
         * to have first point(depot) in cycle
         */
        for(int binPoints = 1; binPoints < N; ++binPoints){
            
            cycleSize = BinArrayCaster.CountSelectedPoints(binPoints);
            
            if(cycleSize < problem.GetMaxCycleSize()){
                cargo = SumCargo(binPoints);
                
                if( cargo > problem.GetRestrictions().minCycleCargo
                &&  cargo < problem.GetRestrictions().maxCycleCargo ){
                    
                    cycles.add(CreateCycle(binPoints));
                }
            }
        }
        
        return cycles;
    }
        
    private Cycle CreateCycle(int binPoints) {        
        BinArrayCaster baCaster = new BinArrayCaster(binPoints);
        
        return tspSolver.CreateCycle(baCaster.GetArray());
    }
    
    private int CountN() {
        return (int)( Math.pow(2.0, problem.size()) - 1 -
                      Math.pow(2.0, problem.size() - maxCycleSize));
    }
    
    
    
    private int SumCargo(int selectedPoints) {
        int cargo = 0;
        for (int i = 0; i < problem.size(); ++i) {
            cargo += ((int)Math.pow(2,i) & selectedPoints) * problem.GetPoint(i+1).GetDemand();
        }
        return cargo;
    }
    
}
