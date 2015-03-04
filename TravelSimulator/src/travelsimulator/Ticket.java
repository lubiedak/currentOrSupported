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
    String start;
    String stop;
    int distance;
    double ticketCost;
    
    public Ticket(String start, String Stop, int distance){
        this.start = start;
        this.stop = stop;
        this.distance = distance;
        ticketCost = 3 + //base
                     1.25 * distance / 10; //distance fare;
    }
}
