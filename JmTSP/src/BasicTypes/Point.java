package BasicTypes;

/**
 *
 * @author lbiedak
 */
public class Point {
    int id;
    int x;
    int y;
    int demand;
    
    public Point() {
        this.x = 0;
        this.y = 0;
        this.demand = 0;
    }
    public Point(int id, int x, int y, int demand) {
        this.id = id;
        this.x = x;
        this.y = y;
        this.demand = demand;
    }
    
    public Point(int id, int[] xyd) {
        this.id = id;
        this.x = xyd[0];
        this.y = xyd[1];
        this.demand = xyd[2];
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
    
    public int GetId(){
        return id;
    }
    
    @Override
    public String toString(){        
        return "" + x + "\t" + y + "\t" + demand;
    }
}
