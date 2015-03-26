
package tools;

/**
 *
 * @author lbiedak
 */
public class BinArrayCaster {
    int binaryForm;
    Integer[] arrayForm;
    
    public BinArrayCaster(int bin){
        binaryForm = bin;
        BinToArray();
    }
    
    public BinArrayCaster(Integer[] array){
        arrayForm = array;
        ArrayToBin();
    }
    
    public Integer[] GetArray(){
        return arrayForm;
    }
    
    public int GetBinary(){
        return binaryForm;
    }
    
    private void BinToArray(){
        arrayForm = new Integer[CountSelectedPoints(binaryForm)];
        
        int p = 0;
        int b = 1;
        for (int i = 0; i < 32; ++i) {
            if( ( binaryForm & b) != 0){
                arrayForm[p] = i;
                p++;
            }
            b <<= 1;
        }
    }
    
    private void ArrayToBin(){
        binaryForm = 0;
        for (int i : arrayForm) {
            binaryForm += (int)Math.pow(2.0, i);
        }
    }
    
    public static int CountSelectedPoints(int i){
        i = i - ((i >> 1) & 0x55555555);
        i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
        return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
    }
    
    @Override
    public String toString(){
        String str = "ArrayForm:   " + Stringer.ArrayToString(arrayForm);
        
        str +=     "\nBinaryForm:  ";
        int b = 1;
        for (int i = 0; i < 32; ++i) {
            str += (( ( binaryForm & b) != 0) ? 1 : 0);
            b <<= 1;
        }
        
        str +=     "\nIntegerForm: " + binaryForm;
        return str;
    }
}
