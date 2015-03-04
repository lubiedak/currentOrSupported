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
public class Destination {
    String name;
    int startPopularity;
    int stopPopularity;
    
    public Destination(String name, int start, int stop) {
        this.name = name;
        startPopularity = start;
        stopPopularity = stop;
    }
}
