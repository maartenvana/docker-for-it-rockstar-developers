#!/bin/bash

# This makes sure we can actually kill our container when we recieve a sigterm signal
trap "echo Exiting... INT;  exit $?" INT
trap "echo Exiting... TERM; exit $?" TERM
trap "echo Exiting... EXIT; exit $?" EXIT

while true; do

echo "Hello,"
sleep 1
echo "is"
sleep 1
echo "it"
sleep 1
echo "me"
sleep 1
echo "you're"
sleep 1
echo "looking"
sleep 1
echo "for?"

done
