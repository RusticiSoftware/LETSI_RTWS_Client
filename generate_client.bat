echo generating LETSIRTE_Service.cs
echo NOTE: WSE3 must be installed (download from MS), and VS SDK, WSE must be added to your path, eg:
echo C:\Program Files (x86)\Microsoft Visual Studio 8\SDK\v2.0\Bin;C:\Program Files (x86)\Microsoft WSE\v3.0\Tools
echo if you have modified your path, you must close all VS instances before re-building.
echo check build.log (in WSCore project directory) for details if there is an error running the pre-build
echo
echo ***************
echo 
set WSDL_EXE="C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\bin\wsdl.EXE"
set XSDS_DIR="xsds"

%WSDL_EXE%  /n:Rustici.LETSI.rtws.client /out:"LETSIRTE_Client.cs" "%XSDS_DIR%\LETSIRTE_Service.wsdl" "%XSDS_DIR%\LETSIRTETypes.xsd" "%XSDS_DIR%\ieee_1484.11.3-2005.xsd" "%XSDS_DIR%\oasis-200401-wss-wssecurity-secext-1.0-restricted.xsd" "%XSDS_DIR%\oasis-200401-wss-wssecurity-utility-1.0.xsd"


