#!/bin/sh

# find the .csproj.
PWD=`pwd`
cd $1

NUSPEC=`ls *.csproj`
VERSION=`grep -o -p '<Version>.*</Version>' $NUSPEC | sed -n -r "s/^.*<Version>(.*)<\/Version>.*$/\1/p"` 

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

sed -r -i '' -e "s/^(.*)<Version>(.*)<\/Version>.*$/\1<Version>$VERSION<\/Version>/g" $NUSPEC 

cd $PWD

echo 'Build Incremented'