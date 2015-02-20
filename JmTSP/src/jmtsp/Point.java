package jmtsp;

/**
 *
 * @author lbiedak
 */
public class Point {
    int x;
    int y;
    int demand;
    
    public Point()
    {
        x = 0;
        y = 0;
        demand = 0;
    }
    public Point(int x, int y, int demand)
    {
        this.x = x;
        this.y = y;
        this.demand = demand;
    }
}
