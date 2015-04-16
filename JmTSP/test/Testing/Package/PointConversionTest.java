/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Testing.Package;
import BasicTypes.Point;
import BasicTypes.PointRfi;
import static org.junit.Assert.assertEquals;
import org.junit.Test;

/**
 *
 * @author lbiedak
 */
public class PointConversionTest {
    Point point;
    Point middle;
    PointRfi pointRFI;
    
    @Test
    public void XYtoRfi1(){
        point = new Point(2,4,0);
        middle = new Point(-2,0,0);
        pointRFI = new PointRfi(point, middle);
        
        assertEquals("Should be 6", 6, pointRFI.GetR());
        assertEquals("Should be 45", 45, pointRFI.GetFi());
    }
    
    @Test
    public void XYtoRfi2(){
        point = new Point(184,80,0);
        middle = new Point(4,5,0);
        pointRFI = new PointRfi(point, middle);
        
        assertEquals("Should be 195", 195, pointRFI.GetR());
        assertEquals("Should be 23", 23, pointRFI.GetFi());
    }
    
    @Test
    public void XYtoRfi3(){
        point = new Point(100,100,0);
        middle = new Point(200,200,0);
        pointRFI = new PointRfi(point, middle);
        
        assertEquals("Should be 141", 141, pointRFI.GetR());
        assertEquals("Should be 225", 225, pointRFI.GetFi());
    }
    
    @Test
    public void XYtoRfi4(){
        point = new Point(520,350,0);
        middle = new Point(400,400,0);
        pointRFI = new PointRfi(point, middle);
        
        assertEquals("Should be 130", 130, pointRFI.GetR());
        assertEquals("Should be 337", 337, pointRFI.GetFi());
    }
    
}
