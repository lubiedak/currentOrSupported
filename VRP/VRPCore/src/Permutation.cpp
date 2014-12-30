#include "base.h"
#include "Permutation.h"


Permutation::Permutation(void)
{
}


Permutation::~Permutation(void)
{
}

Permutation::Permutation(vUSHORT &vec, int size)
{
    stringstream perm_str;
    for(int i = 0; i < size; ++i)
    {
        perm_str<<vec[i];
    }
    str_ = perm_str.str();
    vec_ = vec;
    size_ = size;
}

void Permutation::Inverse()
{
    stringstream convert;
    convert << str_;
    string y(convert.str());

    str_ = string(y.rbegin(), y.rend());
    vUSHORT vtemp(size_);
    for(int i = 0; i < size_; ++i)
    {
        vtemp[i] = vec_[size_-1-i];
    }
    vec_ = vtemp;
}

void Permutation::Print() const
{

    cout<<str_<<"  "<<vec_[0];
    for(int i = 1; i < size_; ++i)
    {
        cout<<","<<vec_[i];
    }
    cout<<endl;
}
