package jmtsp;

import BasicTypes.Problem;
import inputManager.ProblemCSV;

/**
 *
 * @author lbiedak
 */
public class JmTSP {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        
       /**
        * Chcialbym, zeby bylo tak:
        * ArgParser.checkArguments();
        * Problem = ProblemCreator.CreateProblem()
        * ProblemCreator bylby uniwersalna klasa[fabryka?], ktora w zaleznosci
        * od parametru o rodzaju inputu {json, csv} konstruuje problem
        */
        
        ProblemCSV problemCreator = new ProblemCSV(args[0], args[1], args[2]);
        
        Problem problem = problemCreator.CreateProblem();
        
        System.out.print(problem.toString());
    }
    
}
