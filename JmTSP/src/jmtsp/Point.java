package jmtsp;

/**
 *
 * @author lbiedak
 */
public class Point<T> {
    T x;
    T y;
    T demand;
    
    public Point() {
    }
    public Point(T x, T y, T demand) {
        this.x = x;
        this.y = y;
        this.demand = demand;
    }
}
