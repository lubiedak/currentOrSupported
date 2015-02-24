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
        reader = new CsvReader("testFiles/simpleRowInt.csv", false);
        reader.ReadData();
    }
}
