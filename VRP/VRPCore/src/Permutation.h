#pragma once

#include <iostream>
#include <vector>
#include <sstream>
#include <string>

using namespace std;

class Permutation
{
public:
 Permutation(void);
 Permutation( vUSHORT &vec, int size);
 ~Permutation(void);
 string getStr() const { return str_; };
 void setStr(string s) {str_ = s;};
 vUSHORT getVec() const { return vec_; };
 int getSize() const { return size_; };
 void Inverse();
 void Print() const;

private:
 string str_;
 vUSHORT vec_;
 int size_;
};
