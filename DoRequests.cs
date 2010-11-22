using System;
using System.Collections.Generic;
using System.Text;
using scormenginewsproto.ws020110;
using System.Collections;

namespace LETSI_WS_Stub_Client
{
    class DoRequests_020110 
    {
        public const string EXPRESSION_LANGUAGE = "XPATH";
        #region 020110
        private scormenginewsproto.ws020110.GetValueRequestType CreateGetValueRequest_020110(string xPath, Form1 frm)
        {
            GetValueRequestType getRequest1 = new GetValueRequestType();
            getRequest1.RequestID = frm.GetNextRequestId();
            DataModelElementRefType element = new DataModelElementRefType();
            element.expressionLanguage = EXPRESSION_LANGUAGE;
            element.Value = xPath;
            getRequest1.DataModelElementRef = element;

            return getRequest1;
        }

        private scormenginewsproto.ws020110.SetValueRequestType CreateSetValueRequest_020110(ItemChoiceType dataModelElement, object value, Form1 frm)
        {
            SetValueRequestType setRequest1 = new SetValueRequestType();
            setRequest1.RequestID = frm.GetNextRequestId();

            setRequest1.cmiDataModelElement = new cmiDataModelElement();

            setRequest1.cmiDataModelElement.ItemElementName = dataModelElement;
            setRequest1.cmiDataModelElement.Item = value;

            return setRequest1;
        }

        public void DoDataExchange_020110(Form1 frm)
        {
            frm.Log("Making Data Exchange Request");

            LETSIRTE_Service_020110 ws = new LETSIRTE_Service_020110(frm.GetSessionId(),frm.Secret);

            RTERequestType dataExchangeRequest = new RTERequestType();

            dataExchangeRequest.SessionID = frm.GetSessionId();
            dataExchangeRequest.RequestID = frm.GetNextRequestId();


            ArrayList requests = new ArrayList();

            
            //requests.Add(CreateGetValueRequest_020110("/cocd/completionStatus", frm));
            requests.Add(CreateSetValueRequest_020110(ItemChoiceType.completionStatus, completionStatusType.incomplete, frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/completionStatus", frm));
            //requests.Add(CreateSetValueRequest_020110(ItemChoiceType.completionStatus, completionStatusType.completed, frm));
            //requests.Add(CreateSetValueRequest_020110(ItemChoiceType.completionStatus, completionStatusType.unknown, frm));
           
            //requests.Add(CreateGetValueRequest_020110("/cocd/completionStatus", frm));
            

            //requests.Add(CreateGetValueRequest_020110("/cocd/location", frm));

            
            ////literalString1000Type location = new literalString1000Type();
            ////location.Value = "this is location";
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.location, location, frm));

            //requests.Add(CreateGetValueRequest_020110("/cocd/location", frm));

            
            //requests.Add(CreateGetValueRequest_020110("/cocd/completionStatus", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/completionThreshold", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/credit", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/entry", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/launchData", frm));
            
            //requests.Add(CreateGetValueRequest_020110("/cocd/learnerId", frm));
           
            //requests.Add(CreateGetValueRequest_020110("/cocd/learnerName", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/learnerPreferenceData", frm));
            ////getValueRequests.Add(CreateGetValueRequest("/cocd/learnerPreferenceData/audioLevel", frm));
            ////getValueRequests.Add(CreateGetValueRequest("/cocd/learnerPreferenceData/deliverySpeed", frm));
            ////getValueRequests.Add(CreateGetValueRequest("/cocd/learnerPreferenceData/audioCaptioning", frm));
            ////getValueRequests.Add(CreateGetValueRequest("/cocd/learnerPreferenceData/language", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/lessonStatus", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/location", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/maxTimeAllowed", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/mode", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/progressMeasure", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/rawPassingScore", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/scaledPassingScore", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/score", frm));
            ////getValueRequests.Add(CreateGetValueRequest("/cocd/score/scaled", frm));
            ////getValueRequests.Add(CreateGetValueRequest("/cocd/score/raw", frm));
            ////getValueRequests.Add(CreateGetValueRequest("/cocd/score/min", frm));
            ////getValueRequests.Add(CreateGetValueRequest("/cocd/score/max", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/sessionTime", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/successStatus", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/suspendData", frm));
            //requests.Add(CreateGetValueRequest_020110("/cocd/totalTime", frm));

            //requests.Add(CreateGetValueRequest_020110("/cocd/objectives", frm));
       
            
            //////test set and then get
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.completionStatus, completionStatusType.incomplete, frm));
            ////requests.Add(CreateGetValueRequest_020110("/cocd/completionStatus", frm));
            
            //////test set values

            

            ////comments from learner
            ////commentsFromLearner comments = new commentsFromLearner();
            ////comments.commentFromLearner = new commentType[2];

            ////comments.commentFromLearner[0] = new commentType();
            ////comments.commentFromLearner[0].comment = new localizedString4000Type();
            ////comments.commentFromLearner[0].comment.Value = "first comment";
            ////comments.commentFromLearner[0].comment.lang = "es";
            ////comments.commentFromLearner[0].location = new literalString1000Type();
            ////comments.commentFromLearner[0].location.Value = "1st comment location";
            ////comments.commentFromLearner[0].timeStampSpecified = true;
            ////comments.commentFromLearner[0].timeStamp = new DateTime(2009, 9, 1, 15, 23, 5);

            ////comments.commentFromLearner[1] = new commentType();
            ////comments.commentFromLearner[1].comment = new localizedString4000Type();
            ////comments.commentFromLearner[1].comment.Value = "2nd comment";

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.commentsFromLearner, comments, frm));

            
            //////completion status
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.completionStatus, completionStatusType.unknown, frm));

            //////exit
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.exit, exit.suspend, frm));


            //interactions
            interactions interactions = new interactions();
            interactions.interaction = new interactionType[2];

            interactions.interaction[0] = new interactionType();
            interactions.interaction[0].identifier = "int1";
            interactions.interaction[0].type = interactionTypeType.true_false;
            interactions.interaction[0].weightingSpecified = true;
            interactions.interaction[0].weighting = .9M;
            interactions.interaction[0].timeStampSpecified = true;
            interactions.interaction[0].timeStamp = new DateTime(2009, 9, 1, 15, 23, 5);
            interactions.interaction[0].result = interactionTypeResult.correct;
            interactions.interaction[0].description = new interactionTypeDescription();
            interactions.interaction[0].description.Value = "int 1 description";
            interactions.interaction[0].latency = "PT3S";

            interactions.interaction[0].objectiveIds = new string[1];
            interactions.interaction[0].objectiveIds[0] = "intobj1";

            interactions.interaction[1] = new interactionType();
            interactions.interaction[1].identifier = "int2";
            interactions.interaction[1].type = interactionTypeType.multiple_choice;
            interactions.interaction[1].result = interactionTypeResult.incorrect;

            requests.Add(CreateSetValueRequest_020110(ItemChoiceType.interactions, interactions, frm));


            requests.Add(CreateGetValueRequest_020110("/cocd/interactions", frm));


            //////learner preference
            ////learnerPreferenceType prefs = new learnerPreferenceType();
            ////prefs.audioCaptioningSpecified = true;
            ////prefs.audioCaptioning = learnerPreferenceTypeAudioCaptioning.on;
            ////prefs.audioLevelSpecified = true;
            ////prefs.audioLevel = .9M;
            ////prefs.deliverySpeedSpecified = true;
            ////prefs.deliverySpeed = .2M;
            ////prefs.language = "en-uk";
            //// * 
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.learnerPreferenceData, prefs, frm));
            
            //////location
            ////literalString1000Type location2  = new literalString1000Type();
            ////location.Value = "we're at the first page";
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.location, location2, frm));

            
            //////objectives
            ////objectivesType objectives = new objectivesType();

            ////objectives.objective = new objectiveType[2];

            ////objectives.objective[0] = new objectiveType();
            ////objectives.objective[0].identifier = new longIdentifierType();
            ////objectives.objective[0].identifier.Value = "obj1";
            ////objectives.objective[0].description = new localizedString250Type();
            ////objectives.objective[0].description.Value = "desc of obj1";
            ////objectives.objective[0].description.lang = "en";
            ////objectives.objective[0].completionStatus = completionStatusType.incomplete;
            ////objectives.objective[0].progressMeasure = .2M;
            ////objectives.objective[0].score = new scoreType();
            ////objectives.objective[0].score.maxSpecified = true;
            ////objectives.objective[0].score.max = 100;
            ////objectives.objective[0].score.minSpecified = true;
            ////objectives.objective[0].score.min = 0;
            ////objectives.objective[0].score.rawSpecified = true;
            ////objectives.objective[0].score.raw = 50;
            ////objectives.objective[0].score.scaledSpecified = true;
            ////objectives.objective[0].score.scaled = .5M;
            ////objectives.objective[0].successStatus = successStatusType.failed;

            ////objectives.objective[1] = new objectiveType();
            ////objectives.objective[1].identifier = new longIdentifierType();
            ////objectives.objective[1].identifier.Value = "obj2";
            ////objectives.objective[1].description = new localizedString250Type();
            ////objectives.objective[1].description.Value = "desc of obj2";

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.objectives, objectives, frm));

            

            //////progress measure
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.progressMeasure, .32M, frm));

            
            //////score
            ////scoreType score = new scoreType();

            ////score.maxSpecified = true;
            ////score.max = 20;

            ////score.minSpecified = true;
            ////score.min = 10;

            ////score.rawSpecified = true;
            ////score.raw = 15;

            ////score.scaledSpecified = true;
            ////score.scaled = .5M;

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.score, score, frm));

            
            //////session time
            //////TODO: figure out other session time strings
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.sessionTime, "PT3S", frm));

            //////success status
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.successStatus, successStatusType.passed, frm));

            //////suspend data
            ////literalString4000Type suspendData = new literalString4000Type();
            ////suspendData.Value = "oh suspend can you suspend";
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.suspendData, suspendData, frm));
            ////

            ///***********************************/
            ////begin value tests to check in database

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.completionStatus, completionStatusType.completed, frm));
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.successStatus, successStatusType.passed, frm));

            ////literalString4000Type suspendData = new literalString4000Type();
            ////suspendData.Value = "oh suspend can you suspend";
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.suspendData, suspendData, frm));

            ////literalString1000Type location = new literalString1000Type();
            ////location.Value = "this is location";
            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.location, location, frm));

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.exit, exit.normal, frm));

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.progressMeasure, .64M, frm));

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.sessionTime, "PT100S", frm));

            ////learnerPreferenceType prefs = new learnerPreferenceType();
            ////prefs.audioCaptioningSpecified = true;
            ////prefs.audioCaptioning = learnerPreferenceTypeAudioCaptioning.on;
            ////prefs.audioLevelSpecified = true;
            ////prefs.audioLevel = .9M;
            ////prefs.deliverySpeedSpecified = true;
            ////prefs.deliverySpeed = .2M;
            ////prefs.language = "en-uk";

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.learnerPreferenceData, prefs, frm));


            ////objectivesType objectives = new objectivesType();

            ////objectives.objective = new objectiveType[2];

            ////objectives.objective[0] = new objectiveType();
            ////objectives.objective[0].identifier = new longIdentifierType();
            ////objectives.objective[0].identifier.Value = "obj3";
            ////objectives.objective[0].description = new localizedString250Type();
            ////objectives.objective[0].description.Value = "desc of obj1";
            ////objectives.objective[0].description.lang = "en";
            ////objectives.objective[0].completionStatusSpecified = true;
            ////objectives.objective[0].completionStatus = completionStatusType.incomplete;
            ////objectives.objective[0].progressMeasureSpecified = true;
            ////objectives.objective[0].progressMeasure = .2M;
            ////objectives.objective[0].score = new scoreType();
            ////objectives.objective[0].score.maxSpecified = true;
            ////objectives.objective[0].score.max = 100;
            ////objectives.objective[0].score.minSpecified = true;
            ////objectives.objective[0].score.min = 0;
            ////objectives.objective[0].score.rawSpecified = true;
            ////objectives.objective[0].score.raw = 50;
            ////objectives.objective[0].score.scaledSpecified = true;
            ////objectives.objective[0].score.scaled = .5M;
            ////objectives.objective[0].successStatusSpecified = true;
            ////objectives.objective[0].successStatus = successStatusType.failed;


            ////objectives.objective[1] = new objectiveType();
            ////objectives.objective[1].identifier = new longIdentifierType();
            ////objectives.objective[1].identifier.Value = "obj4";
            ////objectives.objective[1].description = new localizedString250Type();
            ////objectives.objective[1].description.Value = "desc of obj2";

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.objectives, objectives, frm));

            ////commentsFromLearner comments = new commentsFromLearner();
            ////comments.commentFromLearner = new commentType[2];

            ////comments.commentFromLearner[0] = new commentType();

            ////comments.commentFromLearner[0].comment = new localizedString4000Type();
            ////comments.commentFromLearner[0].comment.Value = "first comment";
            ////comments.commentFromLearner[0].comment.lang = "es";
            ////comments.commentFromLearner[0].location = new literalString1000Type();
            ////comments.commentFromLearner[0].location.Value = "1st comment location";
            ////comments.commentFromLearner[0].timeStampSpecified = true;
            ////comments.commentFromLearner[0].timeStamp = new DateTime(2009, 9, 1, 15, 23, 5);

            ////comments.commentFromLearner[1] = new commentType();
            ////comments.commentFromLearner[1].comment = new localizedString4000Type();
            ////comments.commentFromLearner[1].comment.Value = "2nd comment";

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.commentsFromLearner, comments, frm));


            ////interactionsType interactions = new interactionsType();
            ////interactions.interaction = new interactionType[2];

            ////interactions.interaction[0] = new interactionType();
            ////interactions.interaction[0].identifier = new longIdentifierType();
            ////interactions.interaction[0].identifier.Value = "int1";
            ////interactions.interaction[0].type = interactionTypeType.true_false;
            ////interactions.interaction[0].weightingSpecified = true;
            ////interactions.interaction[0].weighting = .9M;
            ////interactions.interaction[0].timeStampSpecified = true;
            ////interactions.interaction[0].timeStamp = new DateTime(2009, 9, 1, 15, 23, 5);
            ////interactions.interaction[0].result = "correct";
            ////interactions.interaction[0].description = new localizedString250Type();
            ////interactions.interaction[0].description.Value = "int 1 description";
            ////interactions.interaction[0].latency = "PT3S";

            ////interactions.interaction[0].objectiveIds = new objectiveIdsType();
            ////interactions.interaction[0].objectiveIds.objectiveId = new longIdentifierType[1];
            ////interactions.interaction[0].objectiveIds.objectiveId[0] = new longIdentifierType();
            ////interactions.interaction[0].objectiveIds.objectiveId[0].Value = "intobj1";

            ////interactions.interaction[1] = new interactionType();
            ////interactions.interaction[1].identifier = new longIdentifierType();
            ////interactions.interaction[1].identifier.Value = "int2";
            ////interactions.interaction[1].type = interactionTypeType.multiple_choice;
            ////interactions.interaction[1].result = "incorrect";

            ////requests.Add(CreateSetValueRequest_020110(ItemChoiceType.interactions, interactions, frm));

            /***********************************/

            dataExchangeRequest.Items = requests.ToArray();

            RTEResponseType dataResponse = ws.RTERequest(dataExchangeRequest);

            string responseText = "";
            responseText += "\tRequestID=" + dataResponse.RequestID + "\r\n";
            responseText += "\tSessionId=" + dataResponse.SessionID + "\r\n\r\n";

            responseText += "\tGetValues:\r\n";
           
            for (int i = 0; i < dataResponse.Items.Length; i++)
            {
                if (dataResponse.Items[i] is GetValueResponseType){
                    GetValueResponseType getResponse = (GetValueResponseType) dataResponse.Items[i];

                    responseText += "\tGetValue Request\r\n";
                    responseText += "\tItemElementName=" + getResponse.cmiDataModelElement.ItemElementName + "\r\n";
                    responseText += "\tItem=" + getResponse.cmiDataModelElement.Item + "\r\n";
                    responseText += "\t\tSuccessIndicator=" + getResponse.SuccessIndicator + "\r\n";
                    responseText += "\t\tRequestID=" + getResponse.RequestID + "\r\n";
                    responseText += "\t\tErrorCode=" + getResponse.ErrorCode + "\r\n";
                    responseText += "\t\tErrorString=" + getResponse.ErrorString + "\r\n";
                    responseText += "\t\tErrorDiagnostic=" + getResponse.ErrorDiagnostic + "\r\n";
                    responseText += "\t\t-----\r\n";
                }
                else if (dataResponse.Items[i] is SetValueResponseType){
                    SetValueResponseType setResponse = (SetValueResponseType) dataResponse.Items[i];
                    
                    responseText += "\tSetValue Request\r\n";
                    responseText += "\tRequestID=" + setResponse.RequestID + "\r\n";
                    responseText += "\t\tSuccessIndicator=" + setResponse.SuccessIndicator + "\r\n";
                    responseText += "\t\tErrorCode=" + setResponse.ErrorCode + "\r\n";
                    responseText += "\t\tErrorString=" + setResponse.ErrorString + "\r\n";
                    responseText += "\t\tErrorDiagnostic=" + setResponse.ErrorDiagnostic + "\r\n";
                    responseText += "\t\t-----\r\n";
                }
                else{
                    responseText += "\tERROR, unrecognized response type\r\n";
                    responseText += "\t\t-----\r\n";
                }
            }

            frm.Log("Server responded with:\r\n" + responseText);

        }

        /*private string CastGetResponseToString(object getResponse)
        {
            if (getResponse is literalString1000Type)
            {
                literalString1000Type resp = (literalString1000Type)getResponse;
                return resp.Value;
            }
            else if (getResponse is literalString250Type)
            {
                literalString250Type resp = (literalString250Type)getResponse;
                return resp.Value;
            }
            else if (getResponse is literalString4000Type)
            {
                literalString4000Type resp = (literalString4000Type)getResponse;
                return resp.Value;
            }
            else if (getResponse is longIdentifierType)
            {
                longIdentifierType resp = (longIdentifierType)getResponse;
                return resp.Value;
            }
            else if (getResponse is localizedString250Type)
            {
                localizedString250Type resp = (localizedString250Type)getResponse;
                return resp.Value;
            }
            else if (getResponse is learnerPreferenceType)
            {
                learnerPreferenceType resp = (learnerPreferenceType)getResponse;
                
                string ret = "Learner Preferences:";

                if (resp.audioCaptioningSpecified) { ret += "\r\n   audio captioning=" + resp.audioCaptioning; }
                if (resp.audioLevelSpecified) { ret += "\r\n   audio level=" + resp.audioLevel; }
                if (resp.deliverySpeedSpecified) { ret += "\r\n   delivery speed=" + resp.deliverySpeed; }

                ret += "\r\n   language=" + resp.language;
                return ret;
            }
            else if (getResponse is scoreType)
            {
                scoreType resp = (scoreType)getResponse;
                string ret = "Score:";

                if (resp.maxSpecified) { ret += "\r\n   max=" + resp.max; }
                if (resp.minSpecified) { ret += "\r\n   min=" + resp.min; }
                if (resp.rawSpecified) { ret += "\r\n   raw=" + resp.raw; }
                if (resp.scaledSpecified) { ret += "\r\n   scaled=" + resp.scaled; }
                return ret;
            }
            else if (getResponse is objectivesType)
            {
                objectivesType resp = (objectivesType)getResponse;

                string ret = "Objectives:";

                for (int i = 0; i < resp.objective.Length; i++)
                {
                    objectiveType obj = resp.objective[i];
                    ret += "\r\n   Objective #1:";
                    
                    ret += "\r\n      id=" + obj.identifier.Value;
                    ret += "\r\n      id=" + obj.description.Value;
                    if (obj.completionStatusSpecified) { ret += "\r\n      completion status=" + obj.completionStatus; }
                    if (obj.progressMeasureSpecified) { ret += "\r\n      progress measure=" + obj.progressMeasure; }
                    if (obj.successStatusSpecified) { ret += "\r\n      success status=" + obj.successStatus; }
                    ret += "\r\n      Score:";
                    if (obj.score.maxSpecified) { ret += "\r\n         max=" + obj.score.max; }
                    if (obj.score.minSpecified) { ret += "\r\n         min=" + obj.score.min; }
                    if (obj.score.rawSpecified) { ret += "\r\n         raw=" + obj.score.raw; }
                    if (obj.score.scaledSpecified) { ret += "\r\n         scaled=" + obj.score.scaled; }
                    return ret;
                }

                return ret;
            }
            else if (getResponse == null)
            {
                return "null";
            }
            {
                return getResponse.ToString();
            }
        }*/

        public void DoInit_020110(Form1 frm)
        {
            LETSIRTE_Service_020110 ws = new LETSIRTE_Service_020110(frm.GetSessionId(),frm.Secret);

            frm.Log("Making Initialize Request");

            InitializeRequestType initRequest = new InitializeRequestType();

            initRequest.RequestID = frm.GetNextRequestId();
            initRequest.SessionID = frm.GetSessionId();

            InitializeResponseType initResponse = ws.Initialize(initRequest);

            string responseText = "";
            responseText += "\tErrorCode=" + initResponse.ErrorCode + "\r\n";
            responseText += "\tErrorString=" + initResponse.ErrorString + "\r\n";
            responseText += "\tErrorDiagnostic=" + initResponse.ErrorDiagnostic + "\r\n";
            responseText += "\tRequestID=" + initResponse.RequestID + "\r\n";
            responseText += "\tSessionID=" + initResponse.SessionID + "\r\n";
            responseText += "\tSuccessIndicator=" + initResponse.SuccessIndicator + "\r\n";

            frm.Log("Server responded with:\r\n" + responseText);
        }

        public void DoTerminate_020110(Form1 frm)
        {
            LETSIRTE_Service_020110 ws = new LETSIRTE_Service_020110(frm.GetSessionId(),frm.Secret);

            frm.Log("Making Terminate Request");

            TerminateRequestType terminateRequest = new TerminateRequestType();
            terminateRequest.RequestID = frm.GetNextRequestId();
            terminateRequest.SessionID = frm.GetSessionId();

            TerminateResponseType terminateResponse = ws.Terminate(terminateRequest);

            string responseText = "";
            responseText += "\tErrorCode=" + terminateResponse.ErrorCode + "\r\n";
            responseText += "\tErrorString=" + terminateResponse.ErrorString + "\r\n";
            responseText += "\tErrorDiagnostic=" + terminateResponse.ErrorDiagnostic + "\r\n";
            responseText += "\tRequestID=" + terminateResponse.RequestID + "\r\n";
            responseText += "\tSessionID=" + terminateResponse.SessionID + "\r\n";
            responseText += "\tSuccessIndicator=" + terminateResponse.SuccessIndicator + "\r\n";

            frm.Log("Server responded with:\r\n" + responseText);
        }
        #endregion
    }
}
