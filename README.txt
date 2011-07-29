This is the Rustici Software test client for LETSI RTWS.

What you'll find here is a precompiled .NET client library for RTWS, a small set of unit tests which use the library,
the schema files you'll need to create libraries in other languages, and finally a couple scripts to generate the library
from those schema files in both .NET and Java.

NOTE:  The tests now close the registration - so you will need to create a new registration, or clear your registration (that's your registration that you created) before each test run.

The current set of tests are designed to exercise the multiple attempt feature.

The values for Registration, Secret, and URL are read from here:  bin\LETSI_WS_Stub_Client.exe.config
