package input.manager;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Map;
import java.util.HashMap;
import java.util.ArrayList;
import java.util.Iterator;

/**
 *
 * @author lbiedak
 */
public class CsvReader 
extends DataReader {
    
    String fileName;
    Map<String, Integer> header;
    Number[][] data;
    boolean firstColumnIsHeader; 
    boolean useDoubleParser;
    String delimiter;
    
    boolean dataLoaded;
    
    public CsvReader(String fileName, boolean firstColumnIsHeader) {
        this.fileName = fileName;
        this.firstColumnIsHeader = firstColumnIsHeader;
        
        useDoubleParser = false;
        
        header = new HashMap<String, Integer>();
        data = new Number[1][];
        
        delimiter = ";";
        dataLoaded = false;
    }
    
    public boolean ReadData() {
        if(FileExists()) {
            ArrayList<String> content = ReadFileContent();
            IsAnyDouble(content);
            CheckDelimiter(content);
            
            if(firstColumnIsHeader) {
                ReadWithColumnHeader(content);
            }else {
                ReadWithRowHeader(content);
            }
            
            dataLoaded = true;
            return true;
        }
        return false;
    }
    
    public boolean FileExists() {
        File f = new File(fileName);
        if(f.exists() && !f.isDirectory())
            return true;
        return false;
    }
    
    public Number GetValue(String key, int n) {
        if(dataLoaded && header.containsKey(key) && n < data.length )
            return data[n][header.get(key)];
        return -1.0;
    }
    
    public int GetIntValue(String key, int n){
        return GetValue(key, n).intValue();
    }
    
    public int GetDataSize() {
        return data.length;
    }
    
    public Number[][] GetData() {
        if(dataLoaded)
            return data;
        return null;
    }
    
    private ArrayList<String> ReadFileContent() {
        ArrayList<String> content = new ArrayList<String>();
        File file = new File(fileName);
        try(BufferedReader br = new BufferedReader(new FileReader(file))) {
            for(String line; (line = br.readLine()) != null; ) {
                content.add(line);
            }
        // line is not visible here.
        }catch (IOException e) {
            e.printStackTrace();
        }
        return content;
    }
    
    private void IsAnyDouble(ArrayList<String> content) {
        for(String cell : content) {
            if(cell.contains(".")){
                useDoubleParser = true;
                return;
            }
        }
        useDoubleParser = false;
    }
    
    private void CheckDelimiter(ArrayList<String> content)
    {
        for(String cell : content) {
            if(cell.contains(";")){
                delimiter = ";";
                return;
            }
        }
        delimiter = ",";
    }
        
    private void ReadWithColumnHeader(ArrayList<String> content) {
        
        data = new Number[content.size()][];
        boolean headerLoaded = false;
        
        int row = 0;
        
        for (String line : content)
        {
            String[] cells = line.split(delimiter);
            data[row] = new Number[cells.length - 1];
            headerLoaded = false;
            
            int col = 0;
            for(String cell : cells) {
                
                if(headerLoaded) {
                    data[row][col++] = useDoubleParser ?
                                       Double.parseDouble(cell)
                                     : Integer.parseInt(cell);
                }else {
                    header.put(cell, row);
                    headerLoaded = true;
                }
            }
            row++;
        }
    }
    
    private void ReadWithRowHeader(ArrayList<String> content) {
        
        data = new Number[content.size() - 1][];
        boolean headerLoaded = false;
        
        int row = -1;
        
        for (String line : content)
        {
            int col = 0;
            
            if(headerLoaded) {
                row++;
                data[row] = new Number[header.size()];
                for(String cell : line.split(delimiter)) {
                    data[row][col++] = useDoubleParser ?
                                       Double.parseDouble(cell)
                                     : Integer.parseInt(cell);
                    if(col >= header.size())
                        break;
                }
            }else {
                for(String cell : line.split(delimiter)) {
                    header.put(cell, col++);
                }
                headerLoaded = true;
            }
            
        }
    }
}
