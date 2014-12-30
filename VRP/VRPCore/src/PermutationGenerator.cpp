#include "base.h"
#include "PermutationGenerator.h"



PermutationGenerator::PermutationGenerator(void)
{
}
PermutationGenerator::PermutationGenerator(UINT N, UINT M) //N=cycle size, M=N_points
{
    N_ = N;
    Nperm_ = Factor(N_)/2;
    permVec_.resize(Nperm_);
    ushortPermVec_.resize( Nperm_ , vUSHORT (N_) );
    combinations_.resize(N_);
    combinationsSum_.resize(N_);
    for(USHORT i=1; i<N_; ++i)
    {
        combinations_[i] = Combination(M, i);
        for( USHORT j = i; j < N_; ++j )
        {
            combinationsSum_[j]+=combinations_[i];
        }
    }
    //workaround, because permuttions are wrongly computed
    WriteToConstVector();
}

PermutationGenerator::~PermutationGenerator(void)
{
}

UINT PermutationGenerator::Factor(USHORT N)
{
    UINT f = 1;
    for( USHORT i = 1; i <= N; ++i )
    {
        f*=i;
    }
    return f;
}

UINT PermutationGenerator::Combination(USHORT n, USHORT k)
{
    UINT comb = 1;
    for( USHORT i = n-k+1; i<=n; ++i)
    {
        comb*=i;
    }
    comb/=Factor(k);
    return comb;
}

void PermutationGenerator::CreatePermVector( bool print )
{
    vector <USHORT> v(N_);
    vector < Permutation > vec1(0);
    string empty_string = "";
    for (int i=0; i<N_; i++)
    {
        v[i]=N_-i-1;
    }
    Permute(v,0, vec1);
    vector < Permutation > vec2 = vec1;

    for(UINT i = 0; i < vec1.size(); i++)
    {
        vec1[i].Inverse();
    }

    for(UINT i = 0; i < vec1.size(); ++i)
    {
        if (vec1[i].getStr()!=empty_string)
        {
            for(UINT j = 0; j < vec1.size(); ++j)
            {
                if (vec2[j].getStr() == vec1[i].getStr())
                {
                    vec1[j].setStr(empty_string);
                    vec2[i].setStr(empty_string);
                }
            }
        }
    }
    int it=0;
    for(UINT i = 0; i < vec2.size(); ++i)
    {
        if (vec1[i].getStr()!=empty_string)
        {
            permVec_[it] = vec1[i];
            ushortPermVec_[it] = vec1[i].getVec(); 
            it++;
        }
    }
    if(print)
    {
        for(UINT i = 0; i < permVec_.size(); ++i)
        {
            permVec_[i].Print();
        }
    }



}


void PermutationGenerator::Permute(vUSHORT &v,int start, vector < Permutation > &vec)
{
   vec.push_back( Permutation( v, N_) );
   if (start < N_)
   {
       int i,j;
       for(i=N_-2; i>=start; i--)
       {
           for(j=i+1; j < N_; j++)
           {
               Swap(v, i, j);
               Permute(v, i+1, vec);
           }
           Rotate_Left(v, i);
       }
   }
}


void PermutationGenerator::Swap(vUSHORT &v,int i,int j)
{
   int t;
   t = v[i];
   v[i] = v[j];
   v[j] = t;
}

void PermutationGenerator::Rotate_Left(vUSHORT &v,int go)
{
   int tmp = v[go];
   for (int i=go; i<N_-1; i++)
   {
       v[i] = v[i+1];
   }
   v[N_-1] = tmp;
}

void PermutationGenerator::WriteToConstVector()
{
    const USHORT perm[60][MAX_CYCLE_SIZE] = {
        {0, 1, 2, 3, 4},
        {0, 2, 1, 3, 4},
        {1, 0, 2, 3, 4},
        {0, 1, 3, 2, 4},
        {0, 2, 3, 1, 4},
        {1, 0, 3, 2, 4},
        {0, 3, 1, 2, 4},
        {0, 3, 2, 1, 4},
        {1, 3, 0, 2, 4},
        {3, 0, 1, 2, 4},
        {3, 0, 2, 1, 4},
        {3, 1, 0, 2, 4},
        {2, 3, 1, 0, 4},
        {1, 3, 2, 0, 4},
        {2, 3, 0, 1, 4},
        {1, 2, 3, 0, 4},
        {3, 2, 1, 0, 4},
        {3, 2, 0, 1, 4},
        {1, 2, 0, 3, 4},
        {2, 1, 3, 0, 4},
        {3, 1, 2, 0, 4},
        {2, 1, 0, 3, 4},
        {2, 0, 3, 1, 4},
        {2, 0, 1, 3, 4},
        {2, 1, 0, 4, 3},
        {2, 1, 4, 0, 3},
        {2, 0, 1, 4, 3},
        {2, 0, 4, 1, 3},
        {2, 4, 0, 1, 3},
        {2, 4, 1, 0, 3},
        {1, 0, 2, 4, 3},
        {1, 0, 4, 2, 3},
        {1, 2, 0, 4, 3},
        {1, 2, 4, 0, 3},
        {1, 4, 2, 0, 3},
        {1, 4, 0, 2, 3},
        {0, 1, 2, 4, 3},
        {0, 1, 4, 2, 3},
        {0, 2, 1, 4, 3},
        {0, 2, 4, 1, 3},
        {0, 4, 2, 1, 3},
        {0, 4, 1, 2, 3},
        {1, 0, 3, 4, 2},
        {1, 0, 4, 3, 2},
        {1, 3, 0, 4, 2},
        {1, 3, 4, 0, 2},
        {1, 4, 3, 0, 2},
        {1, 4, 0, 3, 2},
        {0, 1, 3, 4, 2},
        {0, 1, 4, 3, 2},
        {0, 3, 1, 4, 2},
        {0, 3, 4, 1, 2},
        {0, 4, 3, 1, 2},
        {0, 4, 1, 3, 2},
        {0, 2, 3, 4, 1},
        {0, 2, 4, 3, 1},
        {0, 3, 2, 4, 1},
        {0, 3, 4, 2, 1},
        {0, 4, 2, 3, 1},
        {0, 4, 3, 2, 1}};

    ushortPermVecWritten_ = vvUSHORT(0);
    for(int i = 0; i<60; i++)
    {
        ushortPermVecWritten_.push_back(vUSHORT (perm[i], perm[i]+MAX_CYCLE_SIZE));
    }
    ;
}


