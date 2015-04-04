package inputManager;
import BasicTypes.ProblemRestrictions;
import BasicTypes.Problem;
/**
 *
 * @author lbiedak
 */
public interface ProblemCreator {
    
    Problem CreateProblem();
    
    boolean IsCreatedCorrectly();
}
