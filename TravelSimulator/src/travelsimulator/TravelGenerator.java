/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package travelsimulator;

import java.util.ArrayList;
import java.util.Map;

/**
 *
 * @author lbiedak
 */
public class TravelGenerator {
    ArrayList<Destination> travelLine;
    double density;
    double minLength;
    
    public TravelGenerator(ArrayList<Destination> line, int density, int minLength){
        travelLine = line;
        this.density = density > 0.9 ? 0.9 : density;
        this.minLength = minLength;
    }
    
    public Travel Generate(String name, int cost, int maxPassengers) {
        Travel travel = new Travel(travelLine, cost, maxPassengers);
        
        while(travel.CountDensity() < density)
        {
            
        }
        return travel;
    }
}
