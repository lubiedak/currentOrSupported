
package Testing.Package;
import brute.CyclesCreator;
import brute.MethodLimits;
import BasicTypes.*;
import org.junit.*;
import static org.junit.Assert.*;
/**
 *
 * @author lbiedak
 */
public class CycleCreatorTest {
    
    private CyclesCreator creator;
    private Problem problem;
    private ProblemRestrictions restrictions;
    
    @Test
    public void testMaxCycleSize() {
        problem = new Problem();
        creator = new CyclesCreator(problem);
        
        assertEquals("Default max cycle size is 5", 5, creator.GetMaxCycleSize());
        
        restrictions = new ProblemRestrictions(8,1000,10000);
        problem = new Problem();
        problem.SetRestrictions(restrictions);
        creator = new CyclesCreator(problem);
        
        assertEquals("Absolute max cycle size is 7", MethodLimits.maxCycleSize , creator.GetMaxCycleSize());
    }

}
