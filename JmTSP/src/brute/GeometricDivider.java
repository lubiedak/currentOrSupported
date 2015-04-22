package brute;

import BasicTypes.Point;
import BasicTypes.PointRfi;
import BasicTypes.Problem;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author lbiedak
 */
public class GeometricDivider {
    
    Problem problem;
    int n;
    Point middle;
    PointRfi[] points;
    int sensitivity;
    
    public GeometricDivider(Problem problem, int n){
        this.problem = problem;
        this.n = n;
        this.middle = problem.GetDepot();
        ChangePointsToRfi();
    }
    
    public List<Problem> Divide(int sensitivity){
        this.sensitivity = sensitivity;
        SortPoints();
        ReorganizePoints();
        return new ArrayList<Problem>();
    }
    
    private void ChangePointsToRfi(){
        Point[] problemPoints = problem.GetPoints();
        
        points = new PointRfi[problem.size() - 1];
        for(int i = 1; i < problem.size(); ++i){
            points[i-1] = new PointRfi(problemPoints[i], middle);
        }
    }
    
    private void SortPoints(){
        boolean needToBeSorted = true;
        while(needToBeSorted){
            needToBeSorted = false;
            for(int i = 0; i < points.length - 1; ++i){
                if(points[i].GetFi() > points[i+1].GetFi()){
                    needToBeSorted = true;
                    PointRfi temp = points[i+1];
                    points[i+1] = points[i];
                    points[i] = temp;
                }
            }
        }
    }
    
    private int FindBiggestGap(){
        int biggestGapSize = 360 - points[points.length - 1].GetFi();
        int biggestGap = 0;
        for(int i = 0; i < points.length - 1; ++i){
            if(points[i].GetFi() - points[i+1].GetFi() > biggestGapSize){
                biggestGapSize = points[i].GetFi() - points[i+1].GetFi();
                biggestGap = i+1;
            }
        }
        
        return biggestGap;
    }
    
    private void ReorganizePoints(){
        int gap = FindBiggestGap();
        if (gap != 0){
            PointRfi[] reorPoints = new PointRfi[points.length];
            int iReor = 0;
            for(int i = gap; i < points.length; ++i ){
                reorPoints[iReor] = points[i];
                iReor++;
            }
            for(int i = 0; i < gap; ++i){
                reorPoints[iReor] = points[i];
                iReor++;
            }
        }
    }
    
    private List<Problem> CreateProblems(){
        
        return new ArrayList<Problem>();
    }
}

