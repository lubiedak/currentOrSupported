package Testing.Package;

import input.manager.CsvReader;
import org.junit.*;
import static org.junit.Assert.*;
/**
 *
 * @author lbiedak
 */
public class CsvReaderTest {
    private CsvReader reader;
    
    @Test
    public void testColumnHeaderReading()
    {
        reader = new CsvReader("test/testFiles/simpleRowInt.csv", false);
        assertEquals("Before ReadData() there is no results", -1.0, reader.GetValue("x", 0));
        
        reader.ReadData();
        
        assertEquals("Should be 4.0", 4.0, reader.GetValue("x", 1));
        
        assertEquals("Should be null", -1.0, reader.GetValue("x", 2));
        
        reader = new CsvReader("test/testFiles/simpleRestrictions.csv", true);
        reader.ReadData();
        
        assertEquals("Should be null", -1.0, reader.GetValue("niMa", 0));
        
        assertEquals("Should be 1", 1000, reader.GetIntValue("CycleCargo", 0));
    }
}
