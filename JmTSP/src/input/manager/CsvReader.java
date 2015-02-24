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
    
    public CsvReader(String fileName, boolean firstColumnIsHeader) {
        this.fileName = fileName;
        this.firstColumnIsHeader = firstColumnIsHeader;
        
        useDoubleParser = false;
        
        header = new HashMap<String, Integer>();
        data = new Number[1][];
        
        delimiter = ";";
    }
    
    public boolean ReadData() {
        if(FileExists()) {
            ArrayList<String> content = ReadFileContent();
            IsAnyDouble(content);

            if(firstColumnIsHeader) {
                ReadWithColumnHeader(content);
            }else {
                ReadWithRowHeader(content);
            }
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
            }
        }
        useDoubleParser = false;
    }
        
    private void ReadWithColumnHeader(ArrayList<String> content) {
        
        data = new Number[content.size() - 1][];
        boolean headerLoaded = false;
        
        int row = 0;
        
        for (String line : content)
        {
            String[] cells = line.split(delimiter);
            data[row] = new Number[cells.length - 1];
            
            int col = 0;
            for(String cell : cells) {
                
                if(headerLoaded) {
                    data[row][col++] = useDoubleParser ?
                                       Double.parseDouble(cell)
                                     : Integer.parseInt(cell);
                }else {
                    header.put(cell, row);
                }
            }
            row++;
        }
    }
    
    private void ReadWithRowHeader(ArrayList<String> content) {
        
        data = new Number[content.size() - 1][];
        boolean headerLoaded = false;
        
        int row = 0;
        
        for (String line : content)
        {
            int col = 0;
            
            if(headerLoaded) {
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
