package jmtsp;
import java.util.ArrayList;
/**
 *
 * @author lbiedak
 */
public class Problem {
    
    Point depot;
    ArrayList<Point> points;
    Number[][] distances;
    ProblemRestrictions restrictions;
    public int size;
    
    public Problem()
    {
        depot = new Point();
        points = new ArrayList<Point>();
        distances = new Number[1][];
        restrictions = new ProblemRestrictions();
        size = 0;
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
