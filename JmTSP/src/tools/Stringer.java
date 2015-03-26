
package tools;

/**
 *
 * @author lbiedak
 */
public class Stringer<T> {
    
    public static <T> String ArrayToString(T[] array){
        String str = "";
        for(T i : array){
            str += i + ",";
        }
        return str;
    }
}
