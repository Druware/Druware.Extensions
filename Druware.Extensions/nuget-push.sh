#!/bin/sh

# find the .csproj.
PWD=`pwd`
cd $1

APIKEY=$2
NUSPEC=`ls *.csproj`
VERSION=`grep -o -p '<Version>.*</Version>' $NUSPEC | sed -n -r "s/^.*<Version>(.*)<\/Version>.*$/\1/p"` 
PACKAGE=`ls pub/*.nupkg` 

nuget push -Source --source https://api.nuget.org/v3/index.json -ApiKey $APIKEY $PACKAGE

cd $PWD

echo "Pushed: $PACKAGE"