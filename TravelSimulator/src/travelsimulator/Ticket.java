/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package travelsimulator;

/**
 *
 * @author lbiedak
 */
public class Ticket {
    Destination start;
    Destination stop;
    int distance;
    double ticketCost;
    
    public Ticket(Destination start, Destination Stop){
        this.start = start;
        this.stop = stop;
        distance = start.GetPosition() - stop.GetPosition();
        ticketCost = 3 + //base
                     1.25 * distance / 10; //distance fare;
    }
    
    public int StartPosition(){
        return start.GetPosition();
    }
    
    public int StopPosition(){
        return stop.GetPosition();
    }
    
    public double GetCost() {
        return ticketCost;
    }
    
}
