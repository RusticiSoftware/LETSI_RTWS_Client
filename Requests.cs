using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Windows.Forms;
using System.Diagnostics;
using Rustici.LETSI.rtws.client;
using System.Configuration;
using System.Web.Services.Protocols;
using System.IO;
using System.Xml;
using System.Net;
using System.Threading;

namespace LETSI_WS_Stub_Client
{
    public class RequestCommon
    {
        public string RegId;
        public string Secret;
        public string URL;
        public TextBox output;

        public const decimal NO_CHECK = Decimal.MinValue;

        public RequestCommon()
        {
            RegId = ConfigurationManager.AppSettings.Get("TestRegistration");
            Secret = ConfigurationManager.AppSettings.Get("Secret");
            URL = ConfigurationManager.AppSettings.Get("Endpoint");
        }

        public void Log(string str)
        {
            string msg = Environment.NewLine + str;
            Debug.WriteLine(str);
            if (output != null)
            {
                output.Text += msg + "\r\n";
                output.Refresh();
            }
        }
        /// <summary>
        /// when setting up RTWS, use overload that takes session ID and secret to use WS-Security
        /// </summary>
        protected LETSIRTE_Service getClient()
        {
            LETSIRTE_Service client = new LETSIRTE_Service(RegId, Secret);
            client.Url = URL;
            Debug.WriteLine(URL);

            return client;
        }
    }

    //[TestFixture]
    public class Examples : RequestCommon
    {
        //DE - Not even sure what this is testing, commenting out
        //[Test]
        public void generate()
        {
            LETSIRTE_Service client = getClient();

            Log("verify open attempt");
            attemptSummary[] attempts = client.GetAttemptList();
            if (entry.other == attempts[attempts.Length - 1].entry)
            {
                Assert.Ignore(string.Format("Current attempt for registration {0} is closed, registration is closed, test can't continue.", RegId));
            }

            Requests rq = new Requests();
            rq.interactionsFillIn();
            rq.interactionsLongFillIn();
            rq.interactionsPerformanceTest();
            rq.interactionsSequencing();
            rq.interactionsTest();
            clearTrace();

            // get
            cocdType cmi = client.Get();
            saveTrace("get","");

            // set
            clearTrace();
            cmi.exit = exit.normal;
            cmi.attemptNumberSpecified = false;
            client.Set(cmi);
            saveTrace("set", "");

            // getattemptlist
            clearTrace();
            attempts = client.GetAttemptList();
            saveTrace("getAttemptList", "");

            //getAttempt
            clearTrace();
            client.GetAttempt(1);
            saveTrace("getAttempt", "");
        }

        private void clearTrace()
        {
            File.Delete(@"OutputTrace.webinfo");
            File.Delete(@"InputTrace.webinfo");
        }
        private void saveTrace(string name, string postfix)
        {
            saveTrace(name, true, postfix);
            saveTrace(name, false, postfix);
        }
        private void saveTrace(string name, bool output, string postfix)
        {
            bool done = false;
            string src = (output ? "Out" : "In")+ "putTrace.webinfo";
            string target = ConfigurationManager.AppSettings.Get("SampleOutput") + "\\" + name + (output ? "" : "_response" + postfix) + ".xml";

            File.Delete(target);

            XmlDocument doc = new XmlDocument();
            doc.Load(src);
            XmlNamespaceManager nsm = new XmlNamespaceManager(doc.NameTable);
            nsm.AddNamespace("soap",@"http://schemas.xmlsoap.org/soap/envelope/");
            XmlNodeList nodes = doc.DocumentElement.SelectNodes(@"//soap:Envelope",nsm);
            foreach (XmlElement element in nodes)
            {
                XmlNode header = element.SelectSingleNode(@"soap:Header", nsm);
                if (header != null && header.ChildNodes.Count != 0)
                {
                    Assert.IsFalse(done, "Found more than one node to use as an example for '" + target + "'");
                    // found relevant node
                    XmlDocument formatted = new XmlDocument();
                    formatted.LoadXml(element.OuterXml);

                    XmlTextWriter writer = new XmlTextWriter(target, Encoding.UTF8);
                    writer.Formatting = Formatting.Indented;
                    formatted.Save(writer);
                    //File.WriteAllText(target, element.OuterXml);
                    done = true;
                }
            }
        }
    }


    [TestFixture]
    public class Requests : RequestCommon
    {
        [Test]
        public void init()
        {
            LETSIRTE_Service client = getClient();
        }

        [Test]
        public void get()
        {
            LETSIRTE_Service client = getClient();
            Log (client.Get().ToString());
        }



        [Test]
        public void set()
        {
            LETSIRTE_Service client = getClient();
            cocdType cmi = new cocdType();
            cmi.exitSpecified = true;
            cmi.exit = exit.suspend;

            Log(cmi.ToString());
            client.Set(cmi);
        }

        [Test]
        public void learnerPreferences()
        {
            LETSIRTE_Service client = getClient();
            cocdType cmi = new cocdType();
            cmi.exitSpecified = true;
            cmi.exit = exit.suspend;

            cmi.learnerPreferenceAudioCaptioningSpecified = true;
            cmi.learnerPreferenceAudioCaptioning = learnerPreferenceAudioCaptioning.on;

            cmi.learnerPreferenceAudioLevelSpecified = true;
            cmi.learnerPreferenceAudioLevel = .5m;

            cmi.learnerPreferenceDeliverySpeedSpecified = true;
            cmi.learnerPreferenceDeliverySpeed = .4m;

            cmi.learnerPreferenceLanguage = "en-GB";
            Log(cmi.ToString());
            client.Set(cmi);

            cmi = client.Get();

            Assert.AreEqual(learnerPreferenceAudioCaptioning.on, cmi.learnerPreferenceAudioCaptioning, "learnerPreferenceAudioCaptioning");
            Assert.AreEqual(.5m, cmi.learnerPreferenceAudioLevel, "learnerPreferenceAudioLevel");
            Assert.AreEqual(.4m, cmi.learnerPreferenceDeliverySpeed, "learnerPreferenceDeliverySpeed");
            Assert.AreEqual("en-GB", cmi.learnerPreferenceLanguage, "learnerPreferenceLanguage");
        }

        [Test]
        public void objectiveTest()
        {
            LETSIRTE_Service client = getClient();

            string obj1ID = "test_obj_1";
            string obj2ID = "TeSt_obj_1";

            cocdType cocd = client.Get();
            objectiveType obj = cocd.GetObjective(obj1ID, true);
            obj.description = new objectiveTypeDescription();
            obj.description.Value = "test 1";
            obj.successStatus = successStatusType.passed;
            obj.successStatusSpecified = true;

            obj = cocd.GetObjective(obj2ID, true);
            obj.description = new objectiveTypeDescription();
            obj.description.Value = "really objective 2";
            obj.successStatusSpecified = false;

            //cocd.AddObjective(new objectiveType()); //TODO: test creating and then doing nothing else with an objective -- doesn't work 

            cocd.exit = exit.suspend;
            cocd.exitSpecified = true;
            client.Set(cocd);
            cocd = client.Get();

            obj = cocd.GetObjective(obj2ID, false);
            Assert.IsNotNull(obj, obj2ID);
            Assert.AreEqual(obj2ID, obj.identifier);
            Assert.AreEqual(obj.description.Value, "really objective 2");
            Assert.AreEqual(successStatusType.unknown, obj.successStatus, obj.identifier + "success status ");

            obj = cocd.GetObjective(obj1ID, false);
            Assert.IsNotNull(obj, obj1ID);
            Assert.AreEqual(obj1ID, obj.identifier);
            Assert.AreEqual(obj.description.Value, "test 1");
            Assert.AreEqual(successStatusType.passed, obj.successStatus);
            Assert.AreEqual(true, obj.successStatusSpecified, obj.identifier + "success status specified");
        }

        [Test]
        public void interactionsTest()
        {
            interactionsTest(false);
        }
        [Test]
        public void interactionsTest_Array_vs_Collection_clearing()
        {
            interactionsTest(true);
        }


        private void interactionsTest(bool testArrayClear)
        {
            LETSIRTE_Service client = getClient();

            string int1ID = "test_interaction_1";

            cocdType cocd = client.Get();
            interactionType int1 = cocd.GetInteraction(int1ID,true);
            int1.type = interactionTypeType.fill_in;
            int1.correctResponses = new correctResponsesType();
            int1.correctResponses.ItemsElementName =  new ItemsChoiceType[] {ItemsChoiceType.correctResponseFillIn};
            correctResponsesTypeCorrectResponseFillIn rsp = new correctResponsesTypeCorrectResponseFillIn();
            int1.correctResponses.Items = new object[] { rsp };
            rsp.caseMatters = false;
            rsp.orderMatters = false;
            correctResponsesTypeCorrectResponseFillInMatchText match = new correctResponsesTypeCorrectResponseFillInMatchText();
            correctResponsesTypeCorrectResponseFillInMatchText match2 = new correctResponsesTypeCorrectResponseFillInMatchText();
            rsp.matchText = new correctResponsesTypeCorrectResponseFillInMatchText[] { match, match2 };
            match.Value = "answer 42";
            match2.Value = "answer 2";

            int1.learnerResponse = new learnerResponseType();
            int1.learnerResponse.ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.learnerResponseFillIn };
            learnerResponseTypeLearnerResponseFillIn lrsp = new learnerResponseTypeLearnerResponseFillIn();
            int1.learnerResponse.Items = new object[] { lrsp};
            lrsp.Value = "ANSWER 1";

            int1.result = interactionTypeResult.correct;
            int1.resultSpecified = true;
            int1.timeStamp = DateTime.UtcNow;
            int1.timeStampSpecified = true;

            int1.objectiveIds = new string[] { "id1", "id2" };

            cocd.exitSpecified = true;
            cocd.exit = exit.suspend;
            client.Set(cocd);
            cocd = client.Get();

            int1 = cocd.GetInteraction(int1ID, false);
            Assert.AreEqual(interactionTypeType.fill_in, int1.type);

            Assert.AreEqual(((correctResponsesTypeCorrectResponseFillIn) int1.correctResponses.Items[0]).matchText[0].Value, "answer 42");
            learnerResponseTypeLearnerResponseFillIn lr = (learnerResponseTypeLearnerResponseFillIn) int1.learnerResponse.Items[0];

            Thread.Sleep(1000);

            Assert.AreEqual(lr.Value.ToLower(), "answer 1");
            Assert.AreEqual(interactionTypeResult.correct, int1.result);
            Assert.IsTrue(int1.resultSpecified);
            Assert.Greater(DateTime.UtcNow, int1.timeStamp);
            Assert.IsTrue(int1.timeStampSpecified);
            Assert.AreEqual("id2", int1.objectiveIds[1]);

            if (testArrayClear)
            {
                cocd.exitSpecified = true;
                cocd.exit = exit.suspend;
                int1.learnerResponse.Items = new object[] { };
                int1.objectiveIds = new string[] { };
                client.Set(cocd);
                cocd = client.Get();

                int1 = cocd.GetInteraction(int1ID, false);

                // null or length==0 is acceptable
                if (int1.learnerResponse != null && int1.learnerResponse.Items != null)
                {
                    Assert.AreEqual(0, int1.learnerResponse.Items.Length, "Failed to clear array");
                }
                // null or length==0 is acceptable
                if (int1.objectiveIds != null)
                {
                    Assert.AreEqual(0, int1.objectiveIds.Length, "Failed to clear array");
                }
            }
        }

        [Test]
        public void interactionsPerformanceTest()
        {
            LETSIRTE_Service client = getClient();

            string int2ID = "Performance_interaction_upd";

            cocdType cocd = client.Get();
            interactionType perf = cocd.GetInteraction(int2ID, true);
            perf.type = interactionTypeType.performance;
            perf.correctResponses = new correctResponsesType();
            perf.correctResponses.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.correctResponsePerformance, ItemsChoiceType.correctResponsePerformance };
            correctResponsesTypeCorrectResponsePerformance rsp = new correctResponsesTypeCorrectResponsePerformance();
            correctResponsesTypeCorrectResponsePerformance rsp2 = new correctResponsesTypeCorrectResponsePerformance();
            perf.correctResponses.Items = new object[] { rsp, rsp2 };

            rsp.step = new correctResponsesTypeCorrectResponsePerformanceStep[] {
                new correctResponsesTypeCorrectResponsePerformanceStep("step_1", "answer 1"),
                new correctResponsesTypeCorrectResponsePerformanceStep("step_2_min_42",42M,null)
                };
            rsp2.step = new correctResponsesTypeCorrectResponsePerformanceStep[] {
                new correctResponsesTypeCorrectResponsePerformanceStep("step_1_max_100", null,100M),
                new correctResponsesTypeCorrectResponsePerformanceStep("step_2_minmax_no_limit",null,null)
                };

            perf.description = new interactionTypeDescription();
            perf.description.Value = "performance interaction";
            perf.learnerResponse = new learnerResponseType();
            perf.learnerResponse.ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.learnerResponsePerformance, ItemsChoiceType1.learnerResponsePerformance };
            learnerResponseTypeLearnerResponsePerformance lrsp = new learnerResponseTypeLearnerResponsePerformance();
            learnerResponseTypeLearnerResponsePerformance lrsp2 = new learnerResponseTypeLearnerResponsePerformance();
            perf.learnerResponse.Items = new object[] { lrsp, lrsp2 };
            lrsp.stepName = "step_1";
            lrsp.stepAnswer = new learnerResponseTypeLearnerResponsePerformanceStepAnswer();
            lrsp.stepAnswer.Item = "answer 1";

            lrsp2.stepName = "step_2_min_42";
            lrsp2.stepAnswer = new learnerResponseTypeLearnerResponsePerformanceStepAnswer();
            lrsp2.stepAnswer.Item = 23M; // WRONG!

            perf.result = interactionTypeResult.incorrect;
            perf.resultSpecified = true;

            interactionsPerfValidate(cocd.GetInteraction(int2ID, false));

            cocd.exit = exit.suspend;
            cocd.exitSpecified = true;
            client.Set(cocd);
            cocd = client.Get();

            interactionsPerfValidate(cocd.GetInteraction(int2ID, false));
        }

        [Test]
        public void interactionsLongFillIn()
        {
            LETSIRTE_Service client = getClient();

            string intFillInID = "Long_FillIn";

            cocdType cocd = client.Get();
            interactionType inter = cocd.GetInteraction(intFillInID, true);
            inter.type = interactionTypeType.long_fill_in;
            inter.correctResponses = new correctResponsesType();
            inter.correctResponses.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.correctResponseLongFillIn, ItemsChoiceType.correctResponseLongFillIn };
            correctResponsesTypeCorrectResponseLongFillIn rsp = new correctResponsesTypeCorrectResponseLongFillIn();
            correctResponsesTypeCorrectResponseLongFillIn rsp2 = new correctResponsesTypeCorrectResponseLongFillIn();
            inter.correctResponses.Items = new object[] { rsp, rsp2 };

            rsp.Value = "answer 1";
            rsp2.Value = "answer 2";

            inter.description = new interactionTypeDescription();
            inter.description.Value = "long fillin interaction";
            inter.learnerResponse = new learnerResponseType();
            inter.learnerResponse.ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.learnerResponseLongFillIn};
            learnerResponseTypeLearnerResponseLongFillIn lrsp = new learnerResponseTypeLearnerResponseLongFillIn();
            inter.learnerResponse.Items = new object[] { lrsp};

            lrsp.Value = "answer 2";

            inter.result = interactionTypeResult.correct;
            inter.resultSpecified = true;


            Assert.IsNotNull(inter, "inter");
            Assert.IsNotNull(inter.correctResponses, "inter.correctResponses");
            Assert.IsNotNull(inter.correctResponses.Items, "inter.correctResponses.Items");
            Assert.AreEqual(2, inter.correctResponses.Items.Length, "correct response count");
            Assert.AreEqual(1, inter.learnerResponse.Items.Length, "learner response count");
            Assert.AreEqual("answer 1", ((correctResponsesTypeCorrectResponseLongFillIn)inter.correctResponses.Items[0]).Value, "correct response 1");
            Assert.AreEqual("answer 2", ((correctResponsesTypeCorrectResponseLongFillIn)inter.correctResponses.Items[1]).Value, "correct response 2");
            Assert.AreEqual("answer 2", ((learnerResponseTypeLearnerResponseLongFillIn)inter.learnerResponse.Items[0]).Value, "learner response");


            cocd.exit = exit.suspend;
            cocd.exitSpecified = true;
            client.Set(cocd);
            cocd = client.Get();

            inter = cocd.GetInteraction(intFillInID, false);
            Assert.IsNotNull(inter, "inter");
            Assert.IsNotNull(inter.correctResponses, "inter.correctResponses");
            Assert.IsNotNull(inter.correctResponses.Items, "inter.correctResponses.Items");
            Assert.AreEqual(2, inter.correctResponses.Items.Length, "correct response count");
            Assert.AreEqual(1, inter.learnerResponse.Items.Length , "learner response count");
            Assert.AreEqual("answer 1", ((correctResponsesTypeCorrectResponseLongFillIn)inter.correctResponses.Items[0]).Value, "correct response 1");
            Assert.AreEqual("answer 2", ((correctResponsesTypeCorrectResponseLongFillIn)inter.correctResponses.Items[1]).Value, "correct response 2");
            Assert.AreEqual("answer 2", ((learnerResponseTypeLearnerResponseLongFillIn)inter.learnerResponse.Items[0]).Value, "learner response");

            
        }


        [Test]
        public void interactionsFillIn()
        {
            LETSIRTE_Service client = getClient();

            string intFillInID = "FillIn";

            cocdType cocd = client.Get();
            interactionType inter = cocd.GetInteraction(intFillInID, true);
            inter.type = interactionTypeType.fill_in;
            inter.correctResponses = new correctResponsesType();
            inter.correctResponses.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.correctResponseFillIn, ItemsChoiceType.correctResponseFillIn };
            correctResponsesTypeCorrectResponseFillIn rsp = new correctResponsesTypeCorrectResponseFillIn();
            correctResponsesTypeCorrectResponseFillIn rsp2 = new correctResponsesTypeCorrectResponseFillIn();
            inter.correctResponses.Items = new object[] { rsp, rsp2 };

            rsp.matchText = new correctResponsesTypeCorrectResponseFillInMatchText[2];
            rsp2.matchText = new correctResponsesTypeCorrectResponseFillInMatchText[1];
            rsp.matchText[0] = new correctResponsesTypeCorrectResponseFillInMatchText();
            rsp.matchText[1] = new correctResponsesTypeCorrectResponseFillInMatchText();
            rsp2.matchText[0] = new correctResponsesTypeCorrectResponseFillInMatchText();

            rsp.matchText[0].Value = "answer 1";
            rsp.matchText[1].Value = "answer 1.1";
            rsp2.matchText[0].Value = "answer 2";

            inter.description = new interactionTypeDescription();
            inter.description.Value = "fillin interaction";
            inter.learnerResponse = new learnerResponseType();
            inter.learnerResponse.ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.learnerResponseFillIn, ItemsChoiceType1.learnerResponseFillIn };
            learnerResponseTypeLearnerResponseFillIn lrsp = new learnerResponseTypeLearnerResponseFillIn();
            learnerResponseTypeLearnerResponseFillIn lrsp2 = new learnerResponseTypeLearnerResponseFillIn();
            inter.learnerResponse.Items = new object[] { lrsp,lrsp2 };

            lrsp.Value = "answer 1";
            lrsp2.Value = "answer 1.1";

            inter.result = interactionTypeResult.correct;
            inter.resultSpecified = true;


            Assert.IsNotNull(inter, "inter");
            Assert.IsNotNull(inter.correctResponses, "inter.correctResponses");
            Assert.IsNotNull(inter.correctResponses.Items, "inter.correctResponses.Items");
            Assert.AreEqual(2, inter.correctResponses.Items.Length, "correct response count");
            Assert.AreEqual(2, inter.learnerResponse.Items.Length, "learner response count");
            Assert.AreEqual(2, ((correctResponsesTypeCorrectResponseFillIn)inter.correctResponses.Items[0]).matchText.Length, "correct response 1 match count");
            Assert.AreEqual("answer 1", ((correctResponsesTypeCorrectResponseFillIn)inter.correctResponses.Items[0]).matchText[0].Value, "correct response 1");
            Assert.AreEqual("answer 1.1", ((correctResponsesTypeCorrectResponseFillIn)inter.correctResponses.Items[0]).matchText[1].Value, "correct response 1.1");
            Assert.AreEqual("answer 2", ((correctResponsesTypeCorrectResponseFillIn)inter.correctResponses.Items[1]).matchText[0].Value, "correct response 2");
            Assert.AreEqual("answer 1", ((learnerResponseTypeLearnerResponseFillIn)inter.learnerResponse.Items[0]).Value, "learner response");
            Assert.AreEqual("answer 1.1", ((learnerResponseTypeLearnerResponseFillIn)inter.learnerResponse.Items[1]).Value, "learner response");


            cocd.exit = exit.suspend;
            cocd.exitSpecified = true;
            client.Set(cocd);
            cocd = client.Get();

            inter = cocd.GetInteraction(intFillInID, false);
            Assert.IsNotNull(inter, "inter");
            Assert.IsNotNull(inter.correctResponses, "inter.correctResponses");
            Assert.IsNotNull(inter.correctResponses.Items, "inter.correctResponses.Items");
            Assert.AreEqual(2, inter.correctResponses.Items.Length, "correct response count");
            Assert.AreEqual(2, inter.learnerResponse.Items.Length, "learner response count");
            Assert.AreEqual(2, ((correctResponsesTypeCorrectResponseFillIn)inter.correctResponses.Items[0]).matchText.Length, "correct response 1 match count");
            Assert.AreEqual("answer 1", ((correctResponsesTypeCorrectResponseFillIn)inter.correctResponses.Items[0]).matchText[0].Value, "correct response 1");
            Assert.AreEqual("answer 1.1", ((correctResponsesTypeCorrectResponseFillIn)inter.correctResponses.Items[0]).matchText[1].Value, "correct response 1.1");
            Assert.AreEqual("answer 2", ((correctResponsesTypeCorrectResponseFillIn)inter.correctResponses.Items[1]).matchText[0].Value, "correct response 2");
            Assert.AreEqual("answer 1", ((learnerResponseTypeLearnerResponseFillIn)inter.learnerResponse.Items[0]).Value, "learner response");
            Assert.AreEqual("answer 1.1", ((learnerResponseTypeLearnerResponseFillIn)inter.learnerResponse.Items[1]).Value, "learner response");
        }

        [Test]
        public void interactionsSequencing()
        {
            LETSIRTE_Service client = getClient();

            string intSeqId = "Sequencing";

            cocdType cocd = client.Get();
            interactionType inter = cocd.GetInteraction(intSeqId, true);
            inter.type = interactionTypeType.sequencing;
            inter.correctResponses = new correctResponsesType();
            inter.correctResponses.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.correctResponseSequencing, ItemsChoiceType.correctResponseSequencing };
            correctResponsesTypeCorrectResponseSequencing rsp = new correctResponsesTypeCorrectResponseSequencing();
            correctResponsesTypeCorrectResponseSequencing rsp2 = new correctResponsesTypeCorrectResponseSequencing();
            inter.correctResponses.Items = new object[] { rsp, rsp2 };

            rsp.step = new string[] {"1","2","3","4"};
            rsp2.step = new string[] {"A","B","C"};

            inter.description = new interactionTypeDescription();
            inter.description.Value = "Sequencing interaction";
            inter.learnerResponse = new learnerResponseType();
            inter.learnerResponse.ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.learnerResponseSequencing, ItemsChoiceType1.learnerResponseSequencing };
            learnerResponseTypeLearnerResponseSequencing lrsp = new learnerResponseTypeLearnerResponseSequencing();
            inter.learnerResponse.Items = new object[] { lrsp};

            lrsp.step = new string[] { "A", "B", "C" };

            inter.result = interactionTypeResult.correct;
            inter.resultSpecified = true;

            Assert.IsNotNull(inter, "inter");
            Assert.IsNotNull(inter.correctResponses, "inter.correctResponses");
            Assert.IsNotNull(inter.correctResponses.Items, "inter.correctResponses.Items");
            Assert.AreEqual(2, inter.correctResponses.Items.Length, "correct response count");
            Assert.AreEqual(1, inter.learnerResponse.Items.Length, "learner response count");
            Assert.AreEqual(4, ((correctResponsesTypeCorrectResponseSequencing)inter.correctResponses.Items[0]).step.Length, "correct response 1 step count");
            Assert.AreEqual(3, ((correctResponsesTypeCorrectResponseSequencing)inter.correctResponses.Items[1]).step.Length, "correct response 2 step count");
            Assert.AreEqual("1,2,3,4", string.Join(",", ((correctResponsesTypeCorrectResponseSequencing)inter.correctResponses.Items[0]).step), "correct response 1");
            Assert.AreEqual("A,B,C", string.Join(",", ((correctResponsesTypeCorrectResponseSequencing)inter.correctResponses.Items[1]).step), "correct response 2");
            Assert.AreEqual("A,B,C",string.Join(",",((learnerResponseTypeLearnerResponseSequencing)inter.learnerResponse.Items[0]).step), "learner response");


            cocd.exit = exit.suspend;
            cocd.exitSpecified = true;
            client.Set(cocd);
            cocd = client.Get();

            inter = cocd.GetInteraction(intSeqId, false);
            Assert.IsNotNull(inter, "inter");
            Assert.IsNotNull(inter.correctResponses, "inter.correctResponses");
            Assert.IsNotNull(inter.correctResponses.Items, "inter.correctResponses.Items");
            Assert.AreEqual(2, inter.correctResponses.Items.Length, "correct response count");
            Assert.AreEqual(1, inter.learnerResponse.Items.Length, "learner response count");
            Assert.AreEqual(4, ((correctResponsesTypeCorrectResponseSequencing)inter.correctResponses.Items[0]).step.Length, "correct response 1 step count");
            Assert.AreEqual(3, ((correctResponsesTypeCorrectResponseSequencing)inter.correctResponses.Items[1]).step.Length, "correct response 2 step count");
            Assert.AreEqual("1,2,3,4", string.Join(",", ((correctResponsesTypeCorrectResponseSequencing)inter.correctResponses.Items[0]).step), "correct response 1");
            Assert.AreEqual("A,B,C", string.Join(",", ((correctResponsesTypeCorrectResponseSequencing)inter.correctResponses.Items[1]).step), "correct response 2");
            Assert.AreEqual("A,B,C", string.Join(",", ((learnerResponseTypeLearnerResponseSequencing)inter.learnerResponse.Items[0]).step), "learner response");
        }

        private void interactionsPerfValidate(interactionType rsp)
        {
            Assert.AreEqual(interactionTypeType.performance, rsp.type);
            correctResponsesTypeCorrectResponsePerformance[] responses = new correctResponsesTypeCorrectResponsePerformance[rsp.correctResponses.Items.Length];
            for(int ii = 0;ii<rsp.correctResponses.Items.Length;ii++)
            {
                responses[ii] = (correctResponsesTypeCorrectResponsePerformance)rsp.correctResponses.Items[ii];
            }
            learnerResponseTypeLearnerResponsePerformance[] lResponses = new learnerResponseTypeLearnerResponsePerformance[rsp.learnerResponse.Items.Length];
            for (int ii = 0; ii < rsp.learnerResponse.Items.Length; ii++)
            {
                lResponses[ii] = (learnerResponseTypeLearnerResponsePerformance)rsp.learnerResponse.Items[ii];
            }
            Assert.AreEqual(rsp.result, interactionTypeResult.incorrect);
            Assert.IsTrue(rsp.resultSpecified);

            Assert.AreEqual(2, responses.Length, "response count");
            Assert.AreEqual(2, responses[0].step.Length, "response 1 step count");
            Assert.AreEqual(2, responses[1].step.Length, "response 2 step count");
            Assert.AreEqual(2, rsp.learnerResponse.Items.Length, "learner response count");

            Assert.AreEqual("answer 1", lResponses[0].stepAnswer.Item.ToString(), "learner response 1 step 1 answer");
            Assert.AreEqual("step_1", lResponses[0].stepName, "learner response 1 step 1 name");
            Assert.AreEqual("23", lResponses[1].stepAnswer.Item.ToString(), "learner response 2 step 1 answer");
            Assert.AreEqual("step_2_min_42", lResponses[1].stepName, "learner response 2 step 1 name");

            perfValidateStep(responses[0].step[1], "step_2_min_42", 42M, null, "response 1 step 1");
            perfValidateStep(responses[0].step[0], "step_1", "answer 1", "response 1 step 1");
            perfValidateStep(responses[1].step[0], "step_1_max_100", null, 100M, "response 2 step 1");
            perfValidateStep(responses[1].step[1], "step_2_minmax_no_limit", null, null, "response 2 step 2");
        }
        private void perfValidateStep(correctResponsesTypeCorrectResponsePerformanceStep step, string name , string answer, string msg)
        {
            Assert.AreEqual(name, step.stepName, "step name validation: " + msg);

            Assert.AreEqual(answer, step.stepAnswer.Item, "step answer validation: " + msg);
        }
        private void perfValidateStep(correctResponsesTypeCorrectResponsePerformanceStep step, string name,decimal? min, decimal? max, string msg)
        {
            Assert.AreEqual(name, step.stepName, "step name validation: " + msg);

            Assert.IsAssignableFrom(typeof(correctResponsesTypeCorrectResponsePerformanceStepStepAnswerNumeric), step.stepAnswer.Item,step.stepAnswer.Item.ToString() + " - " + msg);
            correctResponsesTypeCorrectResponsePerformanceStepStepAnswerNumeric numAnswer = (correctResponsesTypeCorrectResponsePerformanceStepStepAnswerNumeric) step.stepAnswer.Item;
            Assert.AreEqual(max != null, numAnswer.maxSpecified, "step max specified valiation: " + msg);
            if (max != null)
            {
                Assert.AreEqual(max.Value, numAnswer.max, "step max value valiation: " + msg);
            }
            Assert.AreEqual(min != null, numAnswer.minSpecified, "step min specified valiation: " + msg);
            if (min != null)
            {
                Assert.AreEqual(min.Value, numAnswer.min, "step min value valiation: " + msg);
            }
        }

        [Test]
        public void commentTest()
        {
            LETSIRTE_Service client = getClient();

            cocdType cocd = new cocdType();
            cocd.commentsFromLearner = new commentType[1];

            cocd.commentsFromLearner[0] = new commentType();

            cocd.commentsFromLearner[0].timeStamp = DateTime.Now;
            cocd.commentsFromLearner[0].location = "right here";
            cocd.commentsFromLearner[0].identifier = "comment1";
            cocd.commentsFromLearner[0].comment = new commentTypeComment();
            cocd.commentsFromLearner[0].comment.Value = "new comment";

            //TODO: empty objective doesn't work cocd.commentsFromLearner[1] = new commentType();

            cocd.exit = exit.suspend;
            cocd.exitSpecified = true;
            client.Set(cocd);
            cocd = client.Get();

            //cocd.commentsFromLearner[0].timeStamp = DateTime.Now;
            //Assert.AreEqual( "right here", cocd.commentsFromLearner[0].location);
            //Assert.AreEqual( "not yet implemented" ,cocd.commentsFromLearner[0].identifier);
            Assert.AreEqual("new comment", cocd.commentsFromLearner[0].comment.Value);
        }

        [Test]
        public void togglePassFail()
        {
            LETSIRTE_Service client = getClient();
            cocdType cmi = client.Get();
            successStatusType status;

            Log("original success status: " + cmi.successStatus.ToString() + " specified: " + cmi.successStatusSpecified);

            cmi.successStatusSpecified = true;
            cmi.successStatus = cmi.successStatus == successStatusType.failed ? successStatusType.passed : successStatusType.failed;
            status = cmi.successStatus;
            Log("success status set to: " + cmi.successStatus);
            cmi.exit = exit.suspend;
            cmi.exitSpecified = true;
            client.Set(cmi);

            cmi = client.Get();
            Assert.IsTrue(cmi.successStatusSpecified, "set success status, therefore should still be marked specified");
            Assert.AreEqual(status, cmi.successStatus, "success status was just set and value not retained");
        }

        [Test]
        public void location()
        {
            LETSIRTE_Service client = getClient();
            Log("verify open attempt");
            attemptSummary[] attempts = client.GetAttemptList();
            if (entry.other == attempts[attempts.Length - 1].entry)
            {
                Assert.Ignore(string.Format("Current attempt for registration {0} is closed, registration is closed, test can't continue.", RegId));
            }

            Log("Location: " + client.Get().location);
            cocdType cocd = new cocdType();
            string location = Guid.NewGuid().ToString();

            cocd.location = location;
            client.Set(cocd);

            cocd = client.Get();
            Assert.AreEqual(location, cocd.location, "location");

            client.Set(new cocdType());
            cocd = client.Get();
            Assert.AreEqual(location, cocd.location, "location");
            
            location = "";

            cocd.location = location;
            client.Set(cocd);

            cocd = client.Get();
            Assert.AreEqual(location, cocd.location, "location");
        }

        [Test]
        public void exit_tst()
        {
            LETSIRTE_Service client = getClient();
            Log("verify open attempt");
            attemptSummary[] attempts = client.GetAttemptList();
            if (entry.other == attempts[attempts.Length - 1].entry)
            {
                Assert.Ignore(string.Format("Current attempt for registration {0} is closed, registration is closed, test can't continue.", RegId));
            }

            // 
            client.Set(new cocdType());
            Assert.AreEqual(entry.resume, client.Get().entry,"entry shoudl be resume after set w/o specified exit (default to suspend).");

        }

    }


    [TestFixture]
    public class MultiAttempt : RequestCommon
    {
        [Test]
        public void workflow()
        {
            LETSIRTE_Service client = getClient();
            Log("verify open attempt");
            attemptSummary[] attempts = client.GetAttemptList();
            if (entry.other == attempts[attempts.Length - 1].entry) {
                Assert.Ignore(string.Format("Current attempt for registration {0} is closed, registration is closed, test can't continue.", RegId));
            }


            //- set values (suspend)
            set(exit.suspend);

            //- clear values (suspend)
            clear(exit.suspend);

            //- set values (normal)
            set(exit.normal);

            //- get list (verify)
            VerifyLast(entry.ab_initio, null, client);

            //- set values (close)
            set(exit.logout);

            //- get list (verify)
            VerifyLast(entry.other, NO_CHECK, client);

            Log(Environment.NewLine + "Trying to update locked registration, should fail.");
            try {
                set(exit.suspend);
                Assert.Fail("Set should have failed due to locked registration.");
            }
            catch (SoapException ex) {
                Log("Expected failure verified: " + ex.Message);
            }
            client = new LETSIRTE_Service(RegId + System.Guid.NewGuid().ToString(), Secret);
            client.Url = URL;

            //- get wrong registration (verify fail)
            Log(Environment.NewLine + "Trying to get attempt list with invalid registration, should fail.");
            try {
                client.GetAttemptList();
                Assert.Fail("Set should have failed due to wrong registration.");
            }
            catch (SoapException ex) {
                Log("Expected failure verified: " + ex.Message);
            }

        }

        private void VerifyLast(entry entryType, decimal? scoreScaled, LETSIRTE_Service client)
        {
            if (scoreScaled != NO_CHECK) {
                Log(string.Format("Verifying last attempt has entry: {0} and scaled score: {1}", entryType, scoreScaled));
            }
            else {
                Log(string.Format("Verifying last attempt has entry: {0}", entryType));
            }
            attemptSummary[] attempts = client.GetAttemptList();
            attemptSummary summary = attempts[attempts.Length - 1];

            Assert.AreEqual(entryType, summary.entry, "Latest attempt entry did not match");
            if (scoreScaled != NO_CHECK) {
                Assert.AreEqual(scoreScaled, summary.scoreScaled, "Latest attempt score scaled did not match");
            }
        }

        [Test]
        public void getAttemptList()
        {
            LETSIRTE_Service client = getClient();
            attemptSummary[] attempts = client.GetAttemptList();
            foreach (attemptSummary att in attempts) {
                Log(att.ToString());
            }
        }


        [Test]
        public void getAttempt()
        {
            LETSIRTE_Service client = getClient();
            Log(client.GetAttempt(2).ToString());
        }

        [Test]
        public void setNormal()
        {
            set(exit.normal);
        }
        private void set(exit exitValue)
        {
            Log(Environment.NewLine + "performing current attempt sets, exit: " + exitValue + Environment.NewLine);
            LETSIRTE_Service client = getClient();
            cocdType cmi = client.Get();

            int expectedLength = cmi.attemptNumber;

            cmi.progressMeasure = (((decimal)expectedLength) + .5M) / 10;
            cmi.progressMeasureSpecified = true;
            cmi.completionStatus = completionStatusType.completed;
            cmi.completionStatusSpecified = true;
            cmi.successStatus = successStatusType.passed;
            cmi.successStatusSpecified = true;
            cmi.scoreScaled = cmi.progressMeasure / 2;
            cmi.scoreScaledSpecified = true;
            cmi.exit = exitValue;
            cmi.exitSpecified = true;

            client.Set(cmi);

            attemptSummary[] attempts = client.GetAttemptList();
            foreach (attemptSummary att in attempts) {
                Log(att.ToString());
            }

            //Did we create a new attempt?
            Boolean exitCreatedNewAttempt = (exitValue == exit.normal || exitValue == exit.Item);
            expectedLength += (exitCreatedNewAttempt ? 1 : 0);
            Assert.AreEqual(expectedLength, attempts.Length);

            attemptSummary latest = attempts[attempts.Length - (1 + (exitCreatedNewAttempt ? 1 : 0))];

            Assert.AreEqual(cmi.scoreScaled, latest.scoreScaled);
            Assert.AreEqual(cmi.successStatus, latest.successStatus);
            Assert.AreEqual(cmi.completionStatus, latest.completionStatus);
            Assert.AreEqual(cmi.progressMeasure, latest.progressMeasure);
            Assert.AreEqual(exitValue == exit.suspend ? entry.resume : entry.other, latest.entry);
            Log("verified set");
        }

        private void clear(exit exitValue)
        {
            Log(Environment.NewLine + "performing current attempt clear, exit: " + exitValue + Environment.NewLine);
            LETSIRTE_Service client = getClient();
            cocdType cmi = client.Get();

            int expectedLength = cmi.attemptNumber;

            cmi.progressMeasure = null;
            cmi.progressMeasureSpecified = true;
            cmi.completionStatus = completionStatusType.unknown;
            cmi.completionStatusSpecified = true;
            cmi.successStatus = successStatusType.unknown;
            cmi.successStatusSpecified = true;
            cmi.scoreScaled = null;
            cmi.scoreScaledSpecified = true;
            cmi.exit = exitValue;
            cmi.exitSpecified = true;

            client.Set(cmi);

            attemptSummary[] attempts = client.GetAttemptList();
            foreach (attemptSummary att in attempts) {
                Log(att.ToString());
            }

            //Did we create a new attempt?
            Boolean exitCreatedNewAttempt = (exitValue == exit.normal || exitValue == exit.Item);
            expectedLength += (exitCreatedNewAttempt ? 1 : 0);

            Assert.AreEqual(expectedLength, attempts.Length);

            attemptSummary latest = attempts[attempts.Length - (1 + (exitCreatedNewAttempt ? 1 : 0))];

            Assert.AreEqual(cmi.scoreScaled, latest.scoreScaled);
            Assert.AreEqual(cmi.successStatus, latest.successStatus);
            Assert.AreEqual(cmi.completionStatus, latest.completionStatus);
            Assert.AreEqual(cmi.progressMeasure, latest.progressMeasure);
            Assert.AreEqual(exitValue == exit.suspend ? entry.resume : entry.other, latest.entry);
            Log("verified clear");
        }

    }


}
