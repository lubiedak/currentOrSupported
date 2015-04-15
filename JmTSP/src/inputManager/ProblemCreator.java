package inputManager;
import jmtsp.ProblemRestrictions;
import jmtsp.Problem;
/**
 *
 * @author lbiedak
 */
public interface ProblemCreator {
    
    Problem CreateProblem();
    
    boolean IsCreatedCorrectly();
}
