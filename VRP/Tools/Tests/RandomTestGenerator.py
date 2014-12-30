import subprocess
import argparse
import os
import basicGeneratorTools as bgt
import time

size = 800
min_r = 100

parser = argparse.ArgumentParser(description='Process some parameters.')
parser.add_argument('--execPath', help='Exec file path')
parser.add_argument('--dir', help='Build or folder dir')
parser.add_argument('--nOfCities', type=int, help='Number of cities')
parser.add_argument('--minD', type=int, help='Minimum demand')
parser.add_argument('--maxD', type=int, help='Maximum demand')

#parsing and setting arguments
args = parser.parse_args()
n = args.nOfCities
nOfGroups = (int)(round(n/20));
workspace_dir = args.dir
exec_path = args.execPath

input_file = "input.csv"
os.makedirs(workspace_dir)

xy = bgt.GeneratePoints(min_r, size, n)
xyd = bgt.GenerateDemands(xy, args.minD, args.maxD)
bgt.ExportToFile(xyd, workspace_dir, input_file)
bgt.DrawPointsImage(xy, size, workspace_dir + '/' + "input.png")

time.sleep(2)

#Executing binary

if( exec_path ):
    arguments = [exec_path, "--points="+workspace_dir + "/"+input_file, "--workspace="+workspace_dir]
    print arguments
    time.sleep(2)
    subprocess.call(arguments)

    #group_division = bgt.ReadCSV(workspace_dir + "/" + "group_division.csv")
    #bgt.DrawPointsImage(group_division, size, workspace_dir + "/" + "group_division.png", True, nOfGroups)
    
    #results = bgt.ReadCSV(workspace_dir + "/result.csv")
    #bgt.DrawConnections(results, workspace_dir + "/results.png", size)
