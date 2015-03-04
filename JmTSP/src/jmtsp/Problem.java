package jmtsp;
import java.util.ArrayList;
/**
 *
 * @author lbiedak
 */
public class Problem {
    
    Point depot;
    public int size;
    ArrayList<Point> points;
    Number[][] distances;
    ProblemRestrictions restrictions;
    
    
    public Problem()
    {
        depot = new Point();
        size = 0;
        points = new ArrayList<Point>();
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
}
