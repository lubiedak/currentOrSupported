package inputManager;
import BasicTypes.*;
/**
 *
 * @author lbiedak
 */
public interface ProblemCreator {
    
    Problem CreateProblem();
    
    boolean IsCreatedCorrectly();
}
