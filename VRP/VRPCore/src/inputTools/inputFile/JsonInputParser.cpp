#include "JsonInputParser.h"

#include <sstream>
#include <fstream>

#include "json/json.h"
#include "DepotBuilder.h"
#include "CityBuilder.h"

JsonInputParser::JsonInputParser()
{
}

JsonInputParser::JsonInputParser(const std::string& filename) :
    filename(filename)
{
}

void JsonInputParser::setInputFile(const std::string& filename)
{
    this->filename = filename;
}

std::string JsonInputParser::getInputFile() const
{
    return filename;
}

bool JsonInputParser::parse()
{
    Json::Reader reader;
    Json::Value root;

    logI("Parsing started.");
    logI("Reading file...");

    std::ifstream file(filename, std::ifstream::binary);
    
    bool parsingFileSuccessful = reader.parse(file, root, false);
    if(!parsingFileSuccessful)
    {
        logE(reader.getFormattedErrorMessages());
        logI("File not read due to errors.");
        logI("Parsing finished.");

        return false;
    }

    logI("Done.");
    logI("Reading data...");

    bool parsingDataSuccessful = parseData(root);
    if(!parsingDataSuccessful)
    {
        logI("Data not read due to errors.");
        logI("Parsing finished.");

        return false;
    }

    logI("Done.");
    logI("Parsing finished.");

    return true;
}

bool JsonInputParser::parseData(const Json::Value& rootJ)
{
    /*
     * Sample JSON:
     *
     * {
     *  "depots": [
     *             {
     *              "id" : 1,
     *              "name" : "Gdańsk",
     *              "coordinate" : {
     *                              "x" : 10,
     *                              "y" : 20
     *                             }
     *             }
     *            ],
     *  "cities" : [
     *              {
     *               "id" : 2,
     *               "name" : "Gdynia",
     *               "coordinate" : {
     *                               "x":50,
     *                               "y":60
     *                              },
     *               "demand" : 15,
     *               "isFirst" : true,
     *               "isLast" : false,
     *               "isStop" : false,
     *               "depotId" : 1
     *              }
     *             ]
     * }
     *
     */

    // Get depots
    const Json::Value depotsJ = rootJ["depots"];
    if(depotsJ == Json::Value::null)
    {
        logE("No 'depots' element found.");
        return false;
    }

    parseData_depots(depotsJ);

    // Get cities
    const Json::Value citiesJ = rootJ["cities"];
    if(citiesJ == Json::Value::null)
    {
        logE("No 'cities' element found.");
        return false;
    }

    parseData_cities(citiesJ);

    return true;
}

bool JsonInputParser::parseData_depots(const Json::Value& rootJ)
{
    for(int i = 0; i < rootJ.size(); ++i )
    {
        const Json::Value depotJ = rootJ[i];

        if(depotJ["id"] == Json::Value::null)
        {
            logE("No 'id' element found in on of 'depots' child elements.");
            return false;
        }

        DepotBuilder depotBuilder(depotJ.get("id", -1).asInt());

        if(depotJ["name"] == Json::Value::null)
        {
            logE("No 'name' element found in on of 'depots' child elements.");
            return false;
        }

        depotBuilder.setName(depotJ.get("name", "").asString());

        if(depotJ["coordinate"] == Json::Value::null)
        {
            logE("No 'coordinate' element found in one of 'depots' child elements.");
            return false;
        }

        if(depotJ["coordinate"]["x"] == Json::Value::null || depotJ["coordinate"]["y"] == Json::Value::null)
        {
            logE("No 'x' or 'y' element found in 'coordinate' element in one of 'depots' child elements.");
            return false;
        }

        depotBuilder.setPoint(depotJ["coordinate"].get("x", -1).asInt(), depotJ["coordinate"].get("y", -1).asInt());

        try
        {
            depots.push_back(depotBuilder.get());
        }
        catch(std::string& e)
        {
            logE(e);
            return false;
        }
    }

    return true;
}

bool JsonInputParser::parseData_cities(const Json::Value& rootJ)
{
    for(int i = 0; i < rootJ.size(); ++i )
    {
        const Json::Value cityJ = rootJ[i];

        if(cityJ["id"] == Json::Value::null)
        {
            logE("No 'id' element found in on of 'cities' child elements.");
            return false;
        }

        CityBuilder cityBuilder(cityJ.get("id", -1).asInt());

        if(cityJ["name"] == Json::Value::null)
        {
            logE("No 'name' element found in on of 'cities' child elements.");
            return false;
        }

        cityBuilder.setName(cityJ.get("name", "").asString());

        if(cityJ["coordinate"] == Json::Value::null)
        {
            logE("No 'coordinate' element found in one of 'cities' child elements.");
            return false;
        }

        if(cityJ["coordinate"]["x"] == Json::Value::null || cityJ["coordinate"]["y"] == Json::Value::null)
        {
            logE("No 'x' or 'y' element found in 'coordinate' element in one of 'cities' child elements.");
            return false;
        }

        cityBuilder.setPoint(cityJ["coordinate"].get("x", -1).asInt(), cityJ["coordinate"].get("y", -1).asInt());

        if(cityJ["demand"] == Json::Value::null)
        {
            logE("No 'demand' element found in on of 'cities' child elements.");
            return false;
        }

        cityBuilder.setDemand(cityJ.get("demand", "0").asInt());

        if(cityJ["depotId"] == Json::Value::null)
        {
            logE("No 'depotId' element found in on of 'cities' child elements.");
            return false;
        }

        cityBuilder.setDepotId(cityJ.get("depotId", "0").asInt());

        if(cityJ["isFirst"] == Json::Value::null)
        {
            logE("No 'isFirst' element found in on of 'cities' child elements.");
            return false;
        }

        cityBuilder.setIsFirst(cityJ.get("isFirst", false).asBool());

        if(cityJ["isLast"] == Json::Value::null)
        {
            logE("No 'isLast' element found in on of 'cities' child elements.");
            return false;
        }

        cityBuilder.setIsLast(cityJ.get("isLast", false).asBool());

        if(cityJ["isStop"] == Json::Value::null)
        {
            logE("No 'isStop' element found in on of 'cities' child elements.");
            return false;
        }

        cityBuilder.setIsStop(cityJ.get("isStop", false).asBool());

        try
        {
            cities.push_back(cityBuilder.get());
        }
        catch(std::string& e)
        {
            logE(e);
            return false;
        }
    }

    return true;
}

std::string JsonInputParser::getLogs() const
{
    return logs;
}

std::string JsonInputParser::getErrorMessage() const
{
    return errorMessage;
}

void JsonInputParser::logI(const std::string& i)
{
    log(i, INFO);
}

void JsonInputParser::logW(const std::string& w)
{
    log(w, WARNING);
}

void JsonInputParser::logE(const std::string& e)
{
    log(e, ERROR);

    errorMessage = e;
}

void JsonInputParser::log(const std::string& l, LOG_TYPE t)
{
    std::stringstream ssLog;
    ssLog << (char)t << ": " << l << std::endl;
    logs.append(ssLog.str());
}
