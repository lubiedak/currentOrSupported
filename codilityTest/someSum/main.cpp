#include <algorithm>
#include <vector>
#include <iostream>

//sum = A[p] + A[q] + q - p  -->> return max
// 0 <= p <= q < N
long solution(std::vector<int> &A)
{
    int p = 0;
    int q = 0;
    long Ap = 0;
    long Aq = 0;

    long biggest = 0;

    int P,Q;
    
    for (int i = 0; i < A.size(); ++i)
    {
        if(A[i] > Aq && Ap + Aq + i - p > biggest)
        {
            biggest = Ap + Aq + q - p;
            P = p;
            Q = q;
        }
        
        if((A[i] > Ap || A[i] > Aq) && A[i] + A[i] > biggest)
        {
            p = q = i;
            Ap = Aq = A[i];
            biggest = Ap + Aq + q - p;
            
            P = p;
            Q = q;
        }
    }
    std::cout<<"Solution   P: "<<P<<", Q: "<<Q<<", val: "<<biggest<<std::endl;
    return biggest;
};

long solutionBF(std::vector<int> &A)
{
    long biggest = 0;
    long sum = 0;
    int P,Q;
    
    for (int p = 0; p < A.size(); ++p)
    {
        for (int q = 0; q < A.size(); ++q)
        {
            sum = A[p] + A[q] + q - p;
            if(sum > biggest)
            {
                biggest = sum;
                P = p;
                Q = q;
            }
        }   
    }
    std::cout<<"SolutionBF P: "<<P<<", Q: "<<Q<<", val: "<<biggest<<std::endl;
    return biggest;
};


int main()
{
    std::vector<std::vector<int>> test {{1, -3, 3}, // p=q=2, val =6
                                        {-4, 6, 9, -3, 6, 8}}; // p=1, q=??
    std::vector<int> vals = {6,2};
    for(auto v : test)
    {
        solution(v);
        solutionBF(v);
    }
    
}

