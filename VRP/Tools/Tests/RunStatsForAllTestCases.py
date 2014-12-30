import subprocess
import argparse
import os
import basicGeneratorTools as bgt
import time
import ReportCreatorTools as rc

workspaceDir = ""
compareDir = ""

def getLastStatsResultsDir(historyFile):
    dir = ""
    with open(historyFile, 'rb') as f:
        dir = f.read()
        f.close()
    return dir
    
def executeForAllTestCases(testCasesDir, workspaceDir):
    casesTypes = os.listdir(testCasesDir)
    results = ""
    
    for casesType in casesTypes:
        os.mkdir(workspaceDir+casesType)
        cases = os.listdir(testCasesDir+casesType)
        
        for case in cases:
            caseFile = testCasesDir + casesType + "/"+case
            base=os.path.basename( caseFile )
            caseWorkspace = casesType+'/'+os.path.splitext(base)[0]
            os.mkdir(workspaceDir+caseWorkspace)
            
            arguments = [execPath, "--points="+caseFile, "--workspace="+workspaceDir+caseWorkspace]
            resultCode = subprocess.call(arguments, stdout = None)
            
            resultLine = caseWorkspace + ',' + (resultCode == 0 and "Success" or "Failed") + "\n"
            
            results += resultLine
            
    with open(workspaceDir+"testcasesResults.csv", 'w') as f:
        f.write(results)
        f.close()
    return results
    
    
def compareWithPreviousExecutionResults(resultsDir, prevResultsDir):

    results = rc.convertListToDict(rc.readCSVTable( resultsDir + "testcasesResults.csv", False))
    prevResults = rc.convertListToDict(rc.readCSVTable( prevResultsDir + "testcasesResults.csv", False))
    
    compareTable = []
    compareTable.append(["Test filename", os.path.basename(os.path.normpath(resultsDir)), os.path.basename(os.path.normpath(prevResultsDir)), "Is Reggression?"])
    
    for key in results.keys():
        if key in prevResults.keys():
            compareTable.append([key, results[key][0], prevResults[key][0], isRegression(results[key][0], prevResults[key][0])])
        else:
            compareTable.append([key, results[key][0], "Not executed", ""])
        
    return compareTable

def isRegression(now, prev):
    if(now < prev):
        return "<font color =\"Red\"><b> Regression! </b></font>"
    elif(now == prev and now == "Failed"):
        return "<font color =\"orange\"><b> Could be better! </b></font>"
    else:
        return "<font color =\"Green\"><b> OK! </b></font>"
    
def addTablesWithStats(resultsCompare):
    tablesHTML = ""
    for test in resultsCompare[1:]:
        testname = test[0]
        if(test[1] == "Success"):
            if (test[2] == "Success"):
                #runStats
                tablesHTML += "<h2>Run Stats: "+testname+"</h2><br/>"
                tablesHTML += rc.CreateComparedTable(workspaceDir + testname + "/runStats.csv", compareDir + testname + "/runStats.csv", "")
            else:
                tablesHTML += "<h2>Run Stats: "+testname+"</h2><br/>"
                tablesHTML += rc.CreateSimpleTable(workspaceDir + testname + "/runStats.csv")
    return tablesHTML
    
def createResultsHTMLReport(resultsCompare):
    htmlPage = rc.beginOfHTMLPage("Report summary", "Testcases Stats")
    htmlPage += rc.addHTMLTable(resultsCompare)
    htmlPage += addTablesWithStats(resultsCompare)
    htmlPage += rc.endOfHTMLPage()

    with open(workspaceDir+"testcasesResults.htm", 'w') as f:
        f.write(htmlPage)
        f.close()



#------------------ main() ----------------------
parser = argparse.ArgumentParser(description='Process some parameters.')
parser.add_argument('--execPath', help='Exec file path')
parser.add_argument('--statsDir', help='Directory to stats store')
parser.add_argument('--testCasesDir', help='Directory to test cases')
parser.add_argument('--buildNumber', type=int, help='Number of cities')


#parsing and setting arguments
args = parser.parse_args()
buildNumber = args.buildNumber
statsDir = args.statsDir
testCasesDir = args.testCasesDir
execPath = args.execPath
workspaceDir = statsDir + "/" + str(buildNumber) + "/"

os.mkdir(workspaceDir)

#1 get last name of results to compare from history file
compareDir = getLastStatsResultsDir(statsDir+"history.log") + "/"
with open(statsDir+"history.log", 'w') as f:
    f.write(workspaceDir)
    f.close()

#2 execute binary for each testcase - failures and success counting
with open(workspaceDir+"testcasesResults.csv", 'w') as f:
    f.write("")
    f.close()
testResults = executeForAllTestCases(testCasesDir, workspaceDir)

#3 compare for every success with previous version if exist
resultsCompare = compareWithPreviousExecutionResults(workspaceDir, compareDir)

createResultsHTMLReport(resultsCompare)

