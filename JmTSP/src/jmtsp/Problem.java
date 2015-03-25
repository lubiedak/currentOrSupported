package jmtsp;
import java.util.ArrayList;
/**
 *
 * @author lbiedak
 */
public class Problem {

    Point[] points; //first point is depot
    int[][] distances;
    ProblemRestrictions restrictions;
    
    
    public Problem() {
        points = new Point[1];
        distances = new int[1][];
        restrictions = new ProblemRestrictions();
        
    }
    
    public void SetPoints(int[][] xyd) {
        points = new Point[xyd.length];
        int i = 0;
        for(int[] point : xyd){
            points[i++] = new Point(point[0], point[1], point[2]);
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
    
    public Point[] GetSelectedPoints(int selectedPoints, int howMany) {
        Point[] sPoints = new Point[howMany];
        
        int p = 0;
        for (int i = 0; i < size(); ++i) {
            if( ((int)Math.pow(2,i) & selectedPoints) == 1){
                sPoints[p] = points[i];
                p++;
            }
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
        for(Point p : points){
            description += p.toString() + "\n";
        }
        return description;
    }
    
    
}
