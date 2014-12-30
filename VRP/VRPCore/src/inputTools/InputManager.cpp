#include "../base.h"
#include "InputManager.h"
#include <fstream>

#include "inputFile/InputFile.h"

InputManager::InputManager()
{
}

InputManager::InputManager(string path)
{
    file_name = path;
}

InputManager::~InputManager(void)
{
}

bool InputManager::prepareData()
{
    InputFile inputFile(file_name);

    bool readSuccessful = inputFile.read();
    if(!readSuccessful)
    {
        errorMessage = inputFile.getErrorMessage();
        return false;
    }

    //vvSTRING table = ReadFileTable();
    //vvUINT result_table = ConvertTableToUINT(table);
    //data = InputData(result_table);
    vvUINT dataFromFile = inputFile.getData();
    data = InputData(dataFromFile);

    return true;
}
