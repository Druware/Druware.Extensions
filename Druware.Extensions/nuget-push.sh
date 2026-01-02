#!/bin/sh

# find the .csproj.
PWD=`pwd`
cd $1

APIKEY=$2

FN=`ls *.csproj`
if [[ ! -f $FN ]] ; then
    echo 'File is not there, trying alternate location.'
    cd $1
    CWD=`pwd`
    echo "DEBUG: $CWD"
    FN=`ls *.csproj`
    if [[ ! -f $FN ]] ; then
        echo 'File is not there, aborting.'
        exit 1
    fi
fi
echo "File Found"

VERSION=`grep -o -P '<Version>.*</Version>' FN | sed -n -r "s/^.*<Version>(.*)<\/Version>.*$/\1/p"` 
PACKAGE=`ls pub/*.nupkg` 

nuget push -Source --source https://api.nuget.org/v3/index.json -ApiKey $APIKEY $PACKAGE

cd $PWD
echo "Pushed: $PACKAGE"