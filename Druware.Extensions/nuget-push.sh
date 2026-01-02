#!/bin/sh

# find the .nuspec.

APIKEY=$1
NUSPEC=`ls *.nuspec`
VERSION=`grep -o -p '<version>.*</version>' $NUSPEC | sed -n -r "s/^.*<version>(.*)<\/version>.*$/\1/p"` 
PACKAGE=`ls pub/*.nupkg` 

nuget push -Source --source https://api.nuget.org/v3/index.json -ApiKey $APIKEY $PACKAGE

echo "Pushed: $PACKAGE"