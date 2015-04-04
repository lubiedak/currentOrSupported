package BasicTypes;
import java.util.ArrayList;
/**
 *
 * @author lbiedak
 */

public class Problem {
    Point depot;
    Point[] points;
    int[][] distances;
    ProblemRestrictions restrictions;
    
    
    public Problem() {
        depot = new Point();
        points = new Point[1];
        distances = new int[1][];
        restrictions = new ProblemRestrictions();
    }
    
    public void SetPoints(int[][] xyd) {
        points = new Point[xyd.length];
        int i = 0;
        for(int[] point : xyd){
            if(i==0)
                depot = new Point(i++, point);
            else
                points[i-1] = new Point(i++, point);
        }
    }
    
    public void SetRestrictions(ProblemRestrictions restrictions) {
        this.restrictions = restrictions;
    }
    
    public void SetDistances(int[][] dist){
        distances = dist;
    }
    
    public int GetMaxCycleSize() {
        return restrictions.maxCycleSize;
    }
    
    public ProblemRestrictions GetRestrictions() {
        return restrictions;
    }
    
    public Point[] GetPoints() {
        return points;
    }
    
    public Point[] GetSelectedPoints(Integer[] selectedPoints) {
        Point[] sPoints = new Point[selectedPoints.length];
        
        int p = 0;
        for (int i : selectedPoints) {
            sPoints[p] = points[i];
            p++;
        }
        return sPoints;
    }
    
    public Point GetPoint(int index){
        return points[index];
    }
    
    public int size(){
        return points.length;
    }
    
    @Override
    public String toString(){
        String description = "Problem: definition:\n";
        description += restrictions.toString();
        description += "Depot: " + depot + "\n";
        for(Point p : points){
            description += p + "\n";
        }
        return description;
    }
}




