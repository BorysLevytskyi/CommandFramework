echo off
if "%1"=="" (
   echo "Package file is not specified"
   exit
)

echo "Pushing pacakge file %1 to nuget.org" 
nuget push %1 -source https://www.nuget.org 
