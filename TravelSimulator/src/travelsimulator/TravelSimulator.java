package travelsimulator;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/**
 *
 * @author lbiedak
 */
public class TravelSimulator {

    public static ArrayList<Destination> BydZloLine;
    
    public static int MAX_PASSENGERS = 16;
    public static Double density = 0.5;
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        
        InitBydZlo();
        
        TravelGenerator tg = new TravelGenerator(BydZloLine, density);
        for(int i = 0; i < 1; ++i){
            Travel t = tg.Generate(64, MAX_PASSENGERS);
            System.out.print(t);
        }
    }
    
    private static void InitBydZlo() {
        BydZloLine = new ArrayList<Destination>();
        
        BydZloLine.add(new Destination("1Złotów  ", 10, 5, 0));
        BydZloLine.add(new Destination("2Zakrzewo", 2, 2, 10));
        BydZloLine.add(new Destination("3Kujan   ",1,1, 20));
        BydZloLine.add(new Destination("4Sypniewo",2,2, 30));
        BydZloLine.add(new Destination("5Więcbork",4,4, 40));
        BydZloLine.add(new Destination("6Mrocza  ",2,2, 60));
        BydZloLine.add(new Destination("7Nakło   ",1,5, 70));
        BydZloLine.add(new Destination("Bydgoszcz",6,8, 100));
    }
    
}
