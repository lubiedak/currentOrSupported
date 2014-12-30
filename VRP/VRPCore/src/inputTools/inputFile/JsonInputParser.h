#ifndef JSONINPUTPARSER_HEADER
#define JSONINPUTPARSER_HEADER

#include <string>
#include <vector>

#include "json/json-forwards.h"
#include "City.h"
#include "Depot.h"

class JsonInputParser
{
    public:
        std::vector<Depot> depots;
        std::vector<City> cities;

    private:
        std::string filename;

        std::string logs;
        std::string errorMessage;

        enum LOG_TYPE
        {
            INFO = 'I',
            WARNING = 'W',
            ERROR = 'E'
        };

    public:
        JsonInputParser();
        JsonInputParser(const std::string&);

        bool parse();

        void setInputFile(const std::string&);
        std::string getInputFile() const;

        std::string getLogs() const;
        std::string getErrorMessage() const;

    private:
        bool parseData(const Json::Value&);
        bool parseData_depots(const Json::Value&);
        bool parseData_cities(const Json::Value&);

        void logI(const std::string&);
        void logW(const std::string&);
        void logE(const std::string&);
        void log(const std::string&, LOG_TYPE);
};

#endif
