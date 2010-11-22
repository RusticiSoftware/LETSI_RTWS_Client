This is the Rustici Software test client for LETSI RTWS.

NOTE:  The tests now close the registration - so you will need to create a new registration, or clear your registration (that's your registration that you created) before each test run.  The placeto do that is here:

http://dev1.rusticisoftware.com/ScormEngineWS/NoddyLms/NoddyLms.aspx?

The current set of tests are designed to exercise the multiple attempt feature.  The old test client won't work well, as it will create new attempts but is not expecting them -- if you want to run the old tests and are familiar with NUnit, you can load this .exe as an NUnit project and run the "Requests" set of tests.

The values for Registration, Secret, and URL are read from here:  bin\LETSI_WS_Stub_Client.exe.config