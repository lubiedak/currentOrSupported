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
    }
    
    public Point(int x, int y, int demand) {
        id = 0;
        this.x = x;
        this.y = y;
        this.demand = demand;
    }
    
    public Point(int id, int[] xyd) {
        this.id = id;
        if(xyd.length == 3)
        {
            this.x = xyd[0];
            this.y = xyd[1];
            this.demand = xyd[2];
        }
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
