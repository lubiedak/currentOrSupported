/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package travelsimulator;

import java.util.Map;

/**
 *
 * @author lbiedak
 */
public class TravelGenerator {
    Map<String, Integer> travelLine;
    double density;
    double minLength;
    
    public TravelGenerator(Map<String, Integer> line, int density, int minLength){
        travelLine = line;
        this.density = density;
        this.minLength = minLength;
    }
    
    public Travel Generate() {
        Travel travel = new Travel();
        return travel;
    }
}
