#include "base.h"
#include "OutputManager.h"


OutputManager::OutputManager(void)
{
}

OutputManager::OutputManager(string dir, string file)
{
    groupDivisionPath_ = dir + "/group_division.csv";
    resultPath_ = dir + "/result.csv";
}


void OutputManager::ExportToFile(string path, vvUINT data)
{
    ofstream output_file;
    output_file.open (path);
    for(UINT i = 0; i<data.size(); i++)
    {
        for(UINT j = 0; j<data[i].size()-1; j++)
        {
            output_file << data[i][j]<< ",";
        }
        output_file << data[i][data[i].size()-1]<< "\n";
    }
    output_file.close();
}

void OutputManager::ExportGroupDivision(vvUINT data)
{
    ExportToFile(groupDivisionPath_, data);
}

OutputManager::~OutputManager(void)
{
}

void OutputManager::ExportResults(vvUINT data)
{
    ExportToFile(resultPath_, data);
}
