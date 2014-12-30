#ifndef INPUTFILE_HEADER
#define INPUTFILE_HEADER

#include "../../base.h"

#include <string>

class InputFile
{
    public:
        enum FILE_FORMAT
        {
            UNKNOWN,
            CSV,
            JSON
        };

    private:
        std::string filename;
        FILE_FORMAT format;

        vvUINT data;

        std::string errorMessage;

    public:
        InputFile(const std::string&);

        bool read();
        std::string getErrorMessage() const;

        vvUINT getData() const;

    private:
        void detectFormat();
        bool parse();

        bool parseCSV();
        vvSTRING parseCSV_read();
        vvUINT parseCSV_convert(vvSTRING&);

        bool parseJSON();
};

#endif
