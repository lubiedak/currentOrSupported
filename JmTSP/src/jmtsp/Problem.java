package jmtsp;
import java.util.ArrayList;
/**
 *
 * @author lbiedak
 */
public class Problem {

    public int size;
    Point[] points; //first point is depot
    Number[][] distances;
    ProblemRestrictions restrictions;
    
    
    public Problem()
    {
        size = 0;
        points = new Point[1];
        distances = new Number[1][];
        restrictions = new ProblemRestrictions();
        
    }
    
    public Problem(ProblemRestrictions restrictions)
    {
        this.restrictions = restrictions;
    }
    
    public void SetRestrictions(ProblemRestrictions restrictions)
    {
        this.restrictions = restrictions;
    }
    
    public int GetMaxCycleSize()
    {
        return restrictions.maxCycleSize;
    }
    
    public Point[] GetPoints() {
        return points;
    }
}
