package travelsimulator;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/**
 *
 * @author lbiedak
 */
public class TravelSimulator {

    public ArrayList<Destination> BydZloLine;
    
    public int MAX_PASSENGERS = 16;
    public Double density = 0.5;
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
    }
    
    private void InitBydZlo() {
        BydZloLine = new ArrayList<Destination>();
        
        BydZloLine.add(new Destination("Złotów", 5, 5, 0));
        BydZloLine.add(new Destination("Zakrzewo", 2, 2, 30));
        BydZloLine.add(new Destination("Kujan",1,1, 40));
        BydZloLine.add(new Destination("Sypniewo",2,2, 60));
        BydZloLine.add(new Destination("Więcbork",4,4, 70));
        BydZloLine.add(new Destination("Mrocza",2,2, 80));
        BydZloLine.add(new Destination("Nakło",5,5, 90));
        BydZloLine.add(new Destination("Bydgoszcz",8,8, 100));
    }
    
}
