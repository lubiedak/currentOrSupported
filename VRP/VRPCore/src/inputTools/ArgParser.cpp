#include "ArgParser.h"
#include "FileDirChecker.h"

ArgParser::ArgParser(void)
{
}

ArgParser::ArgParser(int argc, char** argv):
    argc_(argc)
{
    argc_-=(argc_>0);
    argv+=(argc_>0);

    option::Stats stats_(usage, argc_, argv);
    options_ = new option::Option[stats_.options_max];
    buffer_  = new option::Option[stats_.buffer_max];
    option::Parser parser_(usage, argc_, argv, options_, buffer_);
}

ArgParser::~ArgParser(void)
{
    delete[] options_;
    delete[] buffer_;
}

bool ArgParser::isParsingSuccessful()
{
    if (parser_.error())
    {
        cout<<"Parsing failed"<<endl;
        return false;
    }
    else
    {
        cout<<"Parsing passed"<<endl;
    }

    if (options_[HELP] || argc_ == 0)
    {
        option::printUsage(std::cout, usage);
        return false;
    }
    return true;
}

bool ArgParser::checkArgumentsCorrectness()
{
    bool success = true;
    for (option::Option* opt = options_[UNKNOWN]; opt; opt = opt->next())
        std::cout << "Unknown option: " << std::string(opt->name,opt->namelen) << "\n";

    for (int i = 0; i < parser_.nonOptionsCount(); ++i)
        std::cout << "Non-option #" << i << ": " << parser_.nonOption(i) << "\n";


    for(int type = POINTS; type <= WORKSPACE; type++)
    {
        option::Option* opt = options_[type];
        if(!opt)
        {
            cout<<"Missing argument type: "<<usage[type].help<<endl;
            if(type == POINTS) //required arguments
            {
                success = false;
                cout<<"ERROR: missing required arguments to run application"<<endl;
            }
        }
    }
    return success;
}

bool ArgParser::checkIfFilesExists()
{
    bool success = true;
    cout<<"Start checking arguments ..."<<endl;

    FileDirChecker fileDirChecker;
    for(int type = POINTS; type < WORKSPACE; type++)
    {
        for( option::Option* opt = options_[type]; opt; opt = opt->next())
        {
            cout<<"Checking: "<<opt->name<<endl;
            if (!fileDirChecker.isFileEnabled(opt->arg))
            {
                cout<<"ERROR: file path: "<<opt->arg<<" for argument: "<<opt->name<<" not exist"<<endl;
                success = false;
            }
        }
    }

    option::Option* opt = options_[WORKSPACE];
    if(!fileDirChecker.isDirEnabled(opt->arg))
    {
        cout<<"ERROR: dir path: "<<opt->arg<<" for argument: "<<opt->name<<" not exist"<<endl;
        success = false;
    }

    return success;
}

bool ArgParser::parse()
{
    bool success = false;
    if( isParsingSuccessful() )
    {
        if( checkArgumentsCorrectness() )
        {
            success = checkIfFilesExists();
        }
    }
    return success;
}


option::Option* ArgParser::getOptions()
{
    return options_;
}
