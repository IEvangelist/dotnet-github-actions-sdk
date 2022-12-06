#!/bin/sh -l

echo "Hello $1"
time=$(date)
echo "time=$time" >> $GITHUB_OUTPUT

$ git update-index --chmod=+x entrypoint.sh
$ git ls-files --stage entrypoint.sh