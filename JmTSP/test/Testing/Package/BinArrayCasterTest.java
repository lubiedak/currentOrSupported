/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Testing.Package;

import static org.junit.Assert.assertArrayEquals;
import static org.junit.Assert.assertEquals;
import org.junit.Test;
import tools.BinArrayCaster;

/**
 *
 * @author lbiedak
 */
public class BinArrayCasterTest {
    BinArrayCaster caster;
    
    @Test
    public void ConversionsTest(){
        caster = new BinArrayCaster(5);
        
        assertArrayEquals("Should be {0,2}", new Integer[]{0,2}, caster.GetArray());
        
        caster = new BinArrayCaster(new Integer[]{5,8,9});
        assertEquals("Should be 800", 800 , caster.GetBinary());
    }
}
