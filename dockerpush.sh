#!/usr/bin/env sh

DOCKERIMAGES=$(docker images | grep testcoders/testautomation  | awk '{ print $1} ' )

for image in $DOCKERIMAGES; do
    PUSH_IMAGE="${image}"
    echo $PUSH_IMAGE
    echo $(docker push $PUSH_IMAGE)
done