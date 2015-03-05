/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package travelsimulator;

import java.util.ArrayList;
import java.util.Map;
import java.util.Random;

/**
 *
 * @author lbiedak
 */
public class TravelGenerator {
    ArrayList<Destination> destinations;
    double density;
    
    int[] startProbabilityLevels;
    int NofDest;
    
    public TravelGenerator(ArrayList<Destination> line, double density){
        destinations = line;
        this.density = density > 0.9 ? 0.9 : density;
        startProbabilityLevels = new int[line.size()];
        NofDest = line.size();
    }
    
    public void InitProbabilityLevels()
    {
        int subsum = 0;
        for(int i = 0; i < startProbabilityLevels.length; ++i) {
            subsum += destinations.get(i).GetStartPopularity();
            startProbabilityLevels[i] = subsum;
        }
    }
    
    public Travel Generate(int cost, int maxPassengers) {
        Travel travel = new Travel(destinations, cost, maxPassengers);
        InitProbabilityLevels();
        while(travel.CountDensity() < density)
        {
            travel.AddTicket(RandomTicket());
        }
        
        return travel;
    }
    
    private Ticket RandomTicket()
    {
        int start = RandomStart();
        int stop = RandomStop(start);
        return new Ticket(destinations.get(start), destinations.get(stop));
    }
    
    private int RandomStart() {
        Random gen = new Random();
        int rand = gen.nextInt(startProbabilityLevels[NofDest-1]);
        int i = 0;
        while(rand > startProbabilityLevels[i]){
            i++;
        };
        if(i == NofDest - 1)
            i--;
        return i;
    }
    
    private int RandomStop(int start) {
        Random gen = new Random();
        int rand = startProbabilityLevels[start] + gen.nextInt(startProbabilityLevels[NofDest-1]
                                     - startProbabilityLevels[start] + 1);
        int i = 0;
        while(rand > startProbabilityLevels[i]){
          i++;  
        };
        if(i == start)
            i++;
        if(i > NofDest-1)
            i = NofDest-1;
        
        return i;
    }
}
