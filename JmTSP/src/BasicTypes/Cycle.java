package BasicTypes;

import java.util.ArrayList;
import brute.PermutationGenerator;

/**
 *
 * @author lbiedak
 */
public class Cycle {

    Point[] points;
    int length;
    int demand;

    public Cycle() {

    }

    public Cycle(Point[] points, int length) {
        this.points = points;
        this.length = length;
        CountDemand();
    }

    public void SetPoints(Point[] points) {
        this.points = points;
        CountDemand();
    }

    private void CountDemand() {
        for (Point p : points) {
            demand += p.GetDemand();
        }
    }

}
