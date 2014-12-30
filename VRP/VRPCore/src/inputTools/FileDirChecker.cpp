#include "FileDirChecker.h"
#include "../base.h"
#include <sys/types.h>
#include <sys/stat.h>

FileDirChecker::FileDirChecker(void)
{
}

FileDirChecker::~FileDirChecker(void)
{
}

bool FileDirChecker::isFileEnabled (const char * name) {
    ifstream f(name);
    return f.good();
}

bool FileDirChecker::isDirEnabled(const char * pathname)
{
    struct stat info;
    bool success = false;
    if( stat( pathname, &info ) != 0 )
    {
        cout<<"cannot access " << pathname<<endl;
    }
    else if( info.st_mode & S_IFDIR )  // S_ISDIR() doesn't exist on my windows
    {
        cout << pathname<<" is a directory"<<endl;
        success = true;
    }
    else
    {
        cout << pathname<<" is not a directory"<<endl;
    }
    return success;
}
