package brute;

import BasicTypes.Point;
import java.util.ArrayList;

/**
 *
 * @author lbiedak
 */
public class GeometricDivider {
    
    Point[] points;
    int n;
    Point middle;
    
    public GeometricDivider(Point[] points, int n){
        middle = points[0];
        this.n = n;
        this.points = new Point[points.length - 1];
        for(int i = 1; i < points.length; ++i){
            this.points[i-1] = points[i];
        }
    }
    
    public GeometricDivider(Point[] points, int n, Point middle){
        this.points = points;
        this.n = n;
        this.middle = middle;
    }
    
    public ArrayList<Point[]> Divide(int sensitivity){
        
        return new ArrayList<Point[]>();
    }
    
    
}

