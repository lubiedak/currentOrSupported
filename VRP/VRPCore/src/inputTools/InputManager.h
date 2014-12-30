#pragma once
#include "InputData.h"

class InputManager
{
public:
    InputManager(void);
    InputManager(string path);
    ~InputManager(void);

    string file_name;

    InputData data;

    string errorMessage;

    bool prepareData();
};

