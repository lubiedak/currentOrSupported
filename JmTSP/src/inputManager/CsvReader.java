package inputManager;

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
    int[][] data;
    boolean firstColumnIsHeader; 
    boolean noHeader;
    String delimiter;
    
    boolean dataLoaded;
    
    public CsvReader(String fileName) {
        this.fileName = fileName;
        firstColumnIsHeader = false;
        noHeader = false;
        
        header = new HashMap<String, Integer>();
        data = new int[1][];
        
        delimiter = ";";
        dataLoaded = false;
    }
    
    public void FirstColumnIsHeader() {
        firstColumnIsHeader = true;
    }
    
    public void NoHeader() {
        noHeader = true;
    }
    
    public boolean ReadData() {
        if(FileExists()) {
            dataLoaded = false;
            ArrayList<String> content = ReadFileContent();
            CheckDelimiter(content);
            
            if(noHeader){
                ReadDataNoHeader(content);
            }else{
                if(firstColumnIsHeader) {
                    ReadWithColumnHeader(content);
                }else {
                    ReadWithRowHeader(content);
                }
            }
            
            dataLoaded = true;
            return dataLoaded;
        }
        return dataLoaded;
    }
    
    public boolean FileExists() {
        File f = new File(fileName);
        if(f.exists() && !f.isDirectory())
            return true;
        return false;
    }
    
    public int GetValue(String key, int n) {
        if(dataLoaded && header.containsKey(key) && n < data.length )
            return data[n][header.get(key)];
        return -1;
    }

    public int GetDataSize() {
        return data.length;
    }
    
    public int[][] GetData() {
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
        }catch (IOException e) {
            e.printStackTrace();
        }
        return content;
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
    
    private void ReadDataNoHeader(ArrayList<String> content) {
        data = new int[content.size()][];
        
        int row = 0;
        for (String line : content)
        {
            int col = 0;
            
            String[] cells = line.split(delimiter);
            data[row] = new int[cells.length];
            
            for(String cell : cells) {
                data[row][col++] = Integer.parseInt(cell);
            }
            row++;
        }
    }
        
    private void ReadWithColumnHeader(ArrayList<String> content) {
        
        String tempLine = content.get(0);
        data = new int[tempLine.split(delimiter).length-1][];
        for(int row = 0; row < data.length; row++){
            data[row] = new int[content.size()];
        }
        
        boolean headerLoaded = false;
        
        int col = 0;
        
        for (String line : content)
        {
            String[] cells = line.split(delimiter);
            headerLoaded = false;
            
            int row = 0;
            for(String cell : cells) {
                
                if(headerLoaded) {
                    data[row++][col] = Integer.parseInt(cell);
                }else {
                    header.put(cell, col);
                    headerLoaded = true;
                    
                }
            }
            col++;
        }
    }
    
    private void ReadWithRowHeader(ArrayList<String> content) {
        
        data = new int[content.size() - 1][];
        boolean headerLoaded = false;
        
        int row = -1;
        
        for (String line : content)
        {
            int col = 0;
            
            if(headerLoaded) {
                row++;
                data[row] = new int[header.size()];
                for(String cell : line.split(delimiter)) {
                    data[row][col++] = Integer.parseInt(cell);
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
