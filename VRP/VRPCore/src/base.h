#ifndef BASE_H
#define BASE_H

#include <stdio.h>
#include <vector>
#include <iostream>
#include <fstream>
#include <string>
#include <sstream>
#include <list>
#include <bitset>
#include <algorithm>
#include <map>

using namespace std;

typedef unsigned short USHORT;
typedef unsigned int UINT;
typedef std::vector < USHORT >    vUSHORT;
typedef std::vector < UINT >    vUINT;
typedef std::vector < int >        vINT;
typedef std::vector < std::string >    vSTRING;
typedef std::vector < double >  vDOUBLE;

typedef std::vector < std::vector < USHORT > >    vvUSHORT;
typedef std::vector < std::vector < UINT > >    vvUINT;
typedef std::vector < std::vector < int > >        vvINT;
typedef std::vector < std::vector < std::string > >    vvSTRING;
typedef std::vector < std::vector < double > >  vvDOUBLE;

const static int MAX_DEMAND = 1000;
#define HALF 16
#define MAX_BITS 32
#define MAX_CYCLE_SIZE 5


template <typename T>
  T StringToNumber ( const string &Text )
  {
     istringstream ss(Text);
     T result;
     return ss >> result ? result : 0;
  }

template <typename T>
  string NumberToString ( T value )
  {
     stringstream ss;
     ss << value;
     return ss.str();
  }

#endif