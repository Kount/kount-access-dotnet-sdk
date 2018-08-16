//-----------------------------------------------------------------------
// <copyright file="RuleEvents.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class definition of RuleEvents
    /// </summary>
    public class RuleEvents
    {
        [JsonProperty("decision")]
        public string Decision { get; set; }
        
        [JsonProperty("total")]
        public int Total { get; set; }

        private List<object> ruleEventsJObjects;

        [JsonProperty("ruleEvents")]
        private List<object> RuleEventsJObjects
        {
            get { return ruleEventsJObjects; }
            set
            {
                SetEventValue(value);
                ruleEventsJObjects = value;
            }
        }

        private List<string> _eventsStr;

        /// <summary>
        /// Available in version 0210 and below
        /// </summary>
        public List<string> Events
        {
            get { return _eventsStr; }
            set { _eventsStr = value; }
        }

        private List<RuleEvent> _eventsObj;

        /// <summary>
        /// Available from version 0400
        /// </summary>
        public List<RuleEvent> RuleEventsData
        {
            get { return _eventsObj; }
            set { _eventsObj = value; }
        }

        private void SetEventValue(List<object> vals)
        {
            if (vals == null || vals.Count < 1) // Actually we do not expect to have Count < 1.
            {
                return;
            }

            try
            {
                var ruleEventsObj = ((Newtonsoft.Json.Linq.JObject)vals[0]).ToObject<RuleEvent>();

                if (string.IsNullOrEmpty(ruleEventsObj.Code) && string.IsNullOrEmpty(ruleEventsObj.Decision) && string.IsNullOrEmpty(ruleEventsObj.Expression))
                {
                    throw new System.Exception();
                }
                else
                {
                    this._eventsObj = new List<RuleEvent>();

                    foreach (var val in vals)
                    {
                        this._eventsObj.Add(((Newtonsoft.Json.Linq.JObject)val).ToObject<RuleEvent>());
                    }

                    return;
                }
            }
            catch (System.Exception)
            {
                // Catch exception and continue
            }

            try
            {
                var ruleEventsStr = ((Newtonsoft.Json.Linq.JObject)vals[0]).ToObject<string>();

                if (string.IsNullOrEmpty(ruleEventsStr))
                {
                    throw new System.Exception();
                }
                else
                {
                    this._eventsStr = new List<string>();

                    foreach (var val in vals)
                    {
                        this._eventsStr.Add(((Newtonsoft.Json.Linq.JObject)val).ToObject<string>());
                    }

                    return;
                }
            }
            catch (System.Exception)
            {
                // Catch exception and continue
            }
        }
    }
}