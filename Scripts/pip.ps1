$ErrorActionPreference = "Stop"

pip3 install Pillow --user
pip3 install pyyaml --user
pip3 install Flask --user
pip3 install flask-restful --user
pip3 install gevent --user

Write-Output "`n`nDone.`n"