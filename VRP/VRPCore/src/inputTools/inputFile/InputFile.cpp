#include "InputFile.h"
#include "JsonInputParser.h"

#include <regex>

InputFile::InputFile(const std::string& filename) :
    filename(filename), format(UNKNOWN)
{
    detectFormat();
}

bool InputFile::read()
{
    return parse();
}

std::string InputFile::getErrorMessage() const
{
    return errorMessage;
}

void InputFile::detectFormat()
{
    // regex for JSON:
    //regex jsonRegex(/\A("([^"\\\\]*|\\\\["\\\\bfnrt\/]|\\\\u[0-9a-f]{4})*"|-?(?=[1-9]|0(?!\d))\d+(\.\d+)?([eE][+-]?\d+)?|true|false|null|\[(?:(?1)(?:,(?1))*)?\s*\]|\{(?:\s*"([^"\\\\]*|\\\\["\\\\bfnrt\/]|\\\\u[0-9a-f]{4})*"\s*:(?1)(?:,\s*"([^"\\\\]*|\\\\["\\\\bfnrt\/]|\\\\u[0-9a-f]{4})*"\s*:(?1))*)?\s*\})\Z/is);

    ifstream myfile (filename);
    char firstCharacter;

    myfile>>firstCharacter;

    if(firstCharacter == '{')
    {
        format = JSON;
    }
    else if(firstCharacter >= '0' && firstCharacter <= '9')
    {
        format = CSV;
    }

    myfile.close();
}

bool InputFile::parse()
{
    switch(format)
    {
    case CSV:
        return parseCSV();

    case JSON:
        return parseJSON();

    case UNKNOWN:
        break;
    };

    return false;
}

bool InputFile::parseCSV()
{
    vvSTRING plainData = parseCSV_read();
    data = parseCSV_convert(plainData);

    return true;
}

vvSTRING InputFile::parseCSV_read()
{
    vvSTRING result;
    string  line;
    string cell;
    ifstream myfile (filename);
    if (myfile.is_open())
    {
        while(getline (myfile, line))
        {
            stringstream ssline(line);
            vSTRING vline(0);
            while(getline(ssline, cell, ','))
            {
                vline.push_back(cell);
            }
            result.push_back(vline);
        }
    }

    return result;
}

vvUINT InputFile::parseCSV_convert(vvSTRING &plainData)
{
    vvUINT convertedData;

    for (vvSTRING::iterator row = plainData.begin(); row != plainData.end(); row++)
    {
        vUINT irow;
        for(vSTRING::iterator col = row->begin(); col != row->end(); col++)
        {
            irow.push_back(StringToNumber<UINT> ( *col ));
        }
        convertedData.push_back(irow);
    }

    return convertedData;
}

bool InputFile::parseJSON()
{
    JsonInputParser jip(filename);
    bool parseSuccessful = jip.parse();
    if(!parseSuccessful)
    {
        errorMessage = jip.getErrorMessage();
        return false;
    }

    std::vector<Depot> depots = jip.depots;
    std::vector<City> cities = jip.cities;

    vvUINT result;

    for(std::vector<Depot>::iterator it = depots.begin(); it != depots.end(); it++)
    {
        vUINT vrow(0);
        vrow.push_back(it->getPoint().getX());
        vrow.push_back(it->getPoint().getY());
        vrow.push_back(0);
        result.push_back(vrow);
    }

    for(std::vector<City>::iterator it = cities.begin(); it != cities.end(); it++)
    {
        vUINT vrow(0);
        vrow.push_back(it->getPoint().getX());
        vrow.push_back(it->getPoint().getY());
        vrow.push_back(it->getDemand());
        result.push_back(vrow);
    }

    data = result;

    return true;
}

vvUINT InputFile::getData() const
{
    return data;
}


