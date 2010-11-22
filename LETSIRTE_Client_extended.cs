using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Web.Services.Protocols;
using System.Collections;
using Rustici.LETSI.rtws;
using Microsoft.Web.Services3.Security.Tokens;
//using Microsoft.Web.Services3.Security.Tokens;
namespace Rustici.LETSI.rtws.client
{
    public partial class LETSIRTE_Service
    {
        public LETSIRTE_Service(string sessionID, string secret)
            : base()
        {

            UsernameToken token = new UsernameToken(sessionID, secret,
                PasswordOption.SendHashed);

            // make credentials available to send with the request
            SetClientCredential(token);
            // indicate that credentials should be sent with the request
            SetPolicy("Client");
        }

        public cocdType Get()
        {
            return Get(new Get()).cocd;
        }

        public attemptSummary[] GetAttemptList()
        {
            return GetAttemptList(new GetAttemptList());
        }

        public void Set(cocdType cmi)
        {
            Set sr = new Set();
            sr.cocd = cmi;
            Set(sr);
        }
        public cocdType GetAttempt(int attemptNumber)
        {
            GetAttempt gar = new GetAttempt();
            gar.attemptNumber = attemptNumber;
            return GetAttempt(gar).cocd;
        }
    }

    public interface IIdentified
    {
       string identifier {
            get;
            set;
        }
    }

    public partial class correctResponsesTypeCorrectResponsePerformanceStep
    {
        public correctResponsesTypeCorrectResponsePerformanceStep() { }

        public correctResponsesTypeCorrectResponsePerformanceStep(string stepName, string stepAnswer)
        {
            this.stepName = stepName;
            this.stepAnswer = new correctResponsesTypeCorrectResponsePerformanceStepStepAnswer();
            this.stepAnswer.Item = stepAnswer;
        }

        public correctResponsesTypeCorrectResponsePerformanceStep(string stepName, Decimal? min, Decimal? max)
        {
            this.stepName = stepName;
            this.stepAnswer = new correctResponsesTypeCorrectResponsePerformanceStepStepAnswer();
            correctResponsesTypeCorrectResponsePerformanceStepStepAnswerNumeric numAnswer = new correctResponsesTypeCorrectResponsePerformanceStepStepAnswerNumeric();
            if (!(min == null))
            {
                numAnswer.minSpecified = true;
                numAnswer.min = min.Value;
            }
            if (!(max == null))
            {
                numAnswer.maxSpecified= true;
                numAnswer.max = max.Value;
            }
            this.stepAnswer.Item = numAnswer;
        }
    }


    public partial class interactionType : IIdentified { }

    public partial class objectiveType : IIdentified { }

    public partial class attemptSummary
    {
        public override string ToString()
        {
            return cocdType.Serialize(this);
        }
    }

    public partial class cocdType
    {
        /// <summary>
        /// returns XML serialization
        /// </summary>
        public override string ToString()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType(),"http://ltsc.ieee.org/xsd/1484_11_3");
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, this);

            return sw.ToString();
        }

        // code in this region is identical between server and client
        // TODO: DRY
        #region SERVER_AND_CLIENT

        public static string Serialize(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType(), "http://ltsc.ieee.org/xsd/1484_11_3");
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, obj);

            return sw.ToString();
        }

        public objectiveType GetObjective(string id, bool createIfNotFound)
        {
            return GetItem<objectiveType>(id, createIfNotFound, ref objectivesField);
        }
        public interactionType GetInteraction(string id, bool createIfNotFound)
        {
            return GetItem<interactionType>(id, createIfNotFound, ref interactionsField);
        }

        private T GetItem<T>(string id, bool createIfNotFound,ref T[] list) where T : class,IIdentified,new()
        {
            foreach (T itm in list)
            {
                if (((IIdentified)itm).identifier == id)
                {
                    return itm;
                }
            }

            if (createIfNotFound)
            {
                // not found, create
                T newItm = new T();
                ((IIdentified)newItm).identifier = id;
                AddItem<T>(newItm,ref list);
                return newItm;
            }
            else
            {
                // not found, not createIfNotFound
                return null;
            }

        }

        public void AddItem<T>(T obj,ref  T[] list)
        {
            int index = 0;
            index = list.Length;
            Array.Resize<T>(ref list, index + 1);

            list[index] = obj;
        }
        #endregion

    }
}
