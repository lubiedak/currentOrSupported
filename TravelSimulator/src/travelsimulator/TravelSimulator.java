package travelsimulator;

import java.util.HashMap;
import java.util.Map;

/**
 *
 * @author lbiedak
 */
public class TravelSimulator {

    public Map<Destination, Integer> BydZloLine;
    
    public int MAX_PASSENGERS = 16;
    public Double density = 0.5;
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
    }
    
    private void InitBydZlo() {
        BydZloLine = new HashMap<Destination, Integer>();
        
        BydZloLine.put(new Destination("Złotów", 5, 5),     100);
        BydZloLine.put(new Destination("Zakrzewo", 2, 2),   90);
        BydZloLine.put(new Destination("Kujan",1,1),        80);
        BydZloLine.put(new Destination("Sypniewo",2,2),     70);
        BydZloLine.put(new Destination("Więcbork",4,4),     60);
        BydZloLine.put(new Destination("Mrocza",2,2),       40);
        BydZloLine.put(new Destination("Nakło",5,5),        30);
        BydZloLine.put(new Destination("Bydgoszcz",8,8),    0);
    }
    
}
