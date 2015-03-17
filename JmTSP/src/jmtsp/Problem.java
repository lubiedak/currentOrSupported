package jmtsp;
import java.util.ArrayList;
/**
 *
 * @author lbiedak
 */
public class Problem {

    public int size;
    Point[] points; //first point is depot
    int[][] distances;
    ProblemRestrictions restrictions;
    
    
    public Problem()
    {
        size = 0;
        points = new Point[1];
        distances = new int[1][];
        restrictions = new ProblemRestrictions();
        
    }
    
    public void SetPoints(int[][] xyd)
    {
        points = new Point[xyd.length];
        int i = 0;
        for(int[] point : xyd){
            points[i++] = new Point(point[0], point[1], point[2]);
        }
    }
    
    public void SetRestrictions(ProblemRestrictions restrictions)
    {
        this.restrictions = restrictions;
    }
    
    public void SetDistances(int[][] dist){
        distances = dist;
    }
    
    public int GetMaxCycleSize()
    {
        return restrictions.maxCycleSize;
    }
    
    public Point[] GetPoints() {
        return points;
    }
}
