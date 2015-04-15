package jmtsp;

/**
 *
 * @author lbiedak
 */
public class Point {
    int x;
    int y;
    int demand;
    
    public Point() {
    }
    public Point(int x, int y, int demand) {
        this.x = x;
        this.y = y;
        this.demand = demand;
    }
    
    public int GetX(){
        return x;
    }
    
    public int GetY(){
        return y;
    }
    
    public int GetDemand(){
        return demand;
    }
    
    @Override
    public String toString(){        
        return "" + x + "\t" + y + "\t" + demand;
    }
}
