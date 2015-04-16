/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package BasicTypes;

/**
 *
 * @author lbiedak
 */
public class PointRfi {
    int r;
    int fi;
    int demand;
    
    public PointRfi(){ }
    
    public PointRfi(Point point, Point middle){
        demand = point.GetDemand();
        CountRandFI(point, middle);
    }
    
    private void CountRandFI(Point point, Point middle){
        double x = (double)point.GetX() - (double)middle.GetX();
        double y = (double)point.GetY() - (double)middle.GetY();
        
        r = (int)Math.sqrt(x*x + y*y);
        
        double pi180 = 180 / Math.PI;
        
        if(x>0 && y>=0)
            fi = (int)Math.round(Math.atan(y/x) * pi180);
        else if(x>0 && y<0)
            fi = (int)Math.round((Math.atan(y/x) + 2*Math.PI) * pi180);
        else if(x<0)
            fi = (int)Math.round((Math.atan(y/x) + Math.PI) * pi180);
        else if(x==0 && y>0)
            fi = 90;
        else if(x==0 && y<0)
            fi = 270;
    }
    
    public int GetR(){
        return r;
    }
    
    public int GetFi(){
        return fi;
    }
    
    @Override
    public String toString(){        
        return "" + r + "\t" + fi + "\t" + demand;
    }
}
