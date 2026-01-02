#!/bin/sh

# If the file is not found, change to the directory and try again
PWD=`pwd`
echo "DEBUG: $PWD"
# find the .csproj.
FN=`ls *.csproj`
echo "DEBUG: $FN"
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

# MacOS
# VERSION=`grep -o -p '<Version>.*</Version>' $FN | sed -n -r "s/^.*<Version>(.*)<\/Version>.*$/\1/p"` 

# Linux
VERSION=`grep -o -P '<Version>.*</Version>' $FN | sed -n -r "s/^.*<Version>(.*)<\/Version>.*$/\1/p"` 

echo "Version Fount"
# parse the number
MAJOR=`echo $VERSION | awk '{split($0,a,"."); print a[1]}'`
MINOR=`echo $VERSION | awk '{split($0,a,"."); print a[2]}'`
REVISION=`echo $VERSION | awk '{split($0,a,"."); print a[3]}'`

# increment the revision
if [ "$1" = '--major' ]; then 
  MAJOR=$((MAJOR+1))
  MINOR=0
  REVISION=0  
elif [ "$1" = '--minor' ]; then
  MINOR=$((MINOR+1))
  REVISION=0
# else increment the revision
else
  REVISION=$((REVISION+1))
fi 
VERSION=$MAJOR.$MINOR.$REVISION
echo "Version Set"
echo "Writing to $FN"
sed -r -i '' -e "s/^(.*)<Version>(.*)<\/Version>.*$/\1<Version>$VERSION<\/Version>/g" $FN 
echo "Version Written"
cd $PWD
echo 'Build Incremented'