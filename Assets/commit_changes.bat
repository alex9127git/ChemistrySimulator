@echo off
git status
echo These are the files going to be commited. Press any key to commit the files
pause
git add .
git commit
git push
echo Commit done. Press any key to close the window
pause