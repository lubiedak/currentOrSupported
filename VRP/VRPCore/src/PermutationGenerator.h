#pragma once
#include <iostream>
#include <vector>
#include <sstream>
#include <string>
#include "Permutation.h"

using namespace std;

class PermutationGenerator
{
public:
    PermutationGenerator(void);
    PermutationGenerator(UINT N, UINT M); //N=cycle size, M=N_points
    ~PermutationGenerator(void);
    void CreatePermVector( bool print = false );
    vector< Permutation > GetVec() const { return permVec_; };
    vvUSHORT GetVecPerm() const { return ushortPermVec_; };
    vvUSHORT GetConstVecPerm() const {return ushortPermVecWritten_;};
    void WriteToConstVector();

private:
    UINT Combination(USHORT n, USHORT k);
    UINT Factor(USHORT N);
    void Permute(vUSHORT &v, int, vector < Permutation > &vec);
    void Print(vUSHORT &v, vector < Permutation > &vec);
    void Swap(vUSHORT &v, int, int);
    void Rotate_Left(vUSHORT &v, int);
    vector< Permutation > permVec_;
    vvUSHORT ushortPermVec_;
    vvUSHORT ushortPermVecWritten_;
    vUINT combinations_;
    vUINT combinationsSum_;
    int N_;
    int Nperm_;
};
