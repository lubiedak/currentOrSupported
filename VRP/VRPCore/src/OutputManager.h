#pragma once
class OutputManager
{
public:
    OutputManager(void);
    OutputManager(string dir, string file);
    ~OutputManager(void);

    void ExportToFile(string path, vvUINT);
    void ExportGroupDivision(vvUINT data);
    void ExportResults(vvUINT data);

    string groupDivisionPath_;
    string resultPath_;
};

