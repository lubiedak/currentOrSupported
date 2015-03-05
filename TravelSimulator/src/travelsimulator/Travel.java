/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package travelsimulator;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Map;
import java.util.Map.Entry;

/**
 *
 * @author lbiedak
 */
public class Travel {
    ArrayList<Destination> destinations;
    String name;
    ArrayList<Ticket> tickets;
    int cost;
    double profit;
    
    
    int[] passengers;
    int maxPassengers;
    
    public Travel(ArrayList<Destination> destinations, int cost, int maxPassengers)
    {
        this.destinations = destinations;
        this.name = destinations.get(0).name + " - " + destinations.get(destinations.size() - 1);
        tickets = new ArrayList<Ticket>();
        this.cost = cost;
        profit = 0;
        
        this.maxPassengers = maxPassengers;
        passengers = new int[destinations.size()];
    }
    
    
    public double CountDensity(){
        Double sum = 0.0;
        for(int p : passengers){
            sum+=p;
        }
        
        return sum/maxPassengers/passengers.length;
    }
    
    public void AddTicket(Ticket ticket){
        if(CanAddNewTicket(ticket)){
            tickets.add(ticket);
            UpdatePassengers(ticket);
            UpdateProfit();
            Collections.sort(tickets);
        }
    }
    
    private boolean CanAddNewTicket(Ticket ticket){
        int i = 0;
        for(Destination d : destinations){
            if(d.GetPosition() >= ticket.StartPosition() && d.GetPosition() <= ticket.StopPosition()){
                if(passengers[i] >= maxPassengers){
                    return false;
                }
            }
            i++;
        }
        
        return true;
    }
    
    private void UpdatePassengers(Ticket ticket){
        int i = 0;
        for(Destination d : destinations){
            if(d.GetPosition() >= ticket.StartPosition() && d.GetPosition() < ticket.StopPosition()){
                passengers[i]++;
            }
            i++;
        }
    }
    
    private void UpdateProfit() {
        profit = 0;
        for(Ticket t : tickets) {
            profit += t.GetCost();
        }
    }
    
    public String toString(){
        
        String s = new String();
        for (Ticket t: tickets){
            s += t.toString() + "\n";
        }
        s+="\n";
        //s+="Ilosc ticketow: " + tickets.size() + " Koszt: " + cost + " Profit: " + profit + "\n";
        return s;
    }
}
