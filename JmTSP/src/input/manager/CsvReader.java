package input.manager;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Map;
import java.util.HashMap;
import java.util.ArrayList;

/**
 *
 * @author lbiedak
 */
public class CsvReader 
extends DataReader {
    
    String fileName;
    Map<String, ArrayList< Object >> data;
    boolean columnHeader;
    boolean useDoubleParser;
    String delimiter;
    
    public CsvReader(String fileName, boolean columnHeader) {
        this.fileName = fileName;
        this.columnHeader = columnHeader;
        
        useDoubleParser = false;
        data = new HashMap<String, ArrayList< Object >>();
        delimiter = "";
    }
    
    public boolean ReadData() {
        String[] content = ReadFileContent();
        IsAnyDouble(content);
        
        if(columnHeader) {
            ReadWithColumnHeader(content);
        }else {
            ReadWithRowHeader(content);
        }
        
        return true;
    }
    
    private String[] ReadFileContent() {
        String content = null;
        File file = new File(fileName);
        try(FileReader reader = new FileReader(file)){
            char[] chars = new char[(int) file.length()];
            reader.read(chars);
            content = new String(chars);
            reader.close();
        }catch (IOException e) {
            e.printStackTrace();
        }
        return content.split(delimiter);
    }
    
    private void IsAnyDouble(String[] content) {
        for(String cell : content) {
            if(cell.contains(".")){
                useDoubleParser = true;
            }
        }
        useDoubleParser = false;
    }
        
    private boolean ReadWithColumnHeader(String[] content) {
        
        for(String cell : content)
        {
            System.out.println(cell);
        }
        return true;
    }
    
    private boolean ReadWithRowHeader(String[] content) {
        return true;
    }
}
