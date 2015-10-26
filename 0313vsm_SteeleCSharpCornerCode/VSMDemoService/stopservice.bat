@echo off
sc query Service1 | find "STATE" | find "RUNNING"
if not .%errorlevel%. == .0. goto skip
net stop Service1

:skip

