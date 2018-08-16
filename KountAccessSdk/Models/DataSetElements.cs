//-----------------------------------------------------------------------
// <copyright file="DataSetElements.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    public class DataSetElements
    {
        private int value = 0;

        const int INFO = 1;
        const int VELOCITY = 2;
        const int DECISION = 4;
        const int TRUSTED = 8;
        const int BEHAVIOSEC = 16;

        public DataSetElements WithInfo()
        {
            this.value = this.value | INFO;
            return this;
        }

        public DataSetElements WithVelocity()
        {
            this.value = this.value | VELOCITY;
            return this;
        }

        public DataSetElements WithDecision()
        {
            this.value = this.value | DECISION;
            return this;
        }

        public DataSetElements WithTrusted()
        {
            this.value = this.value | TRUSTED;
            return this;
        }

        public DataSetElements WithBehavioSec()
        {
            this.value = this.value | BEHAVIOSEC;
            return this;
        }

        public int Build()
        {
            return value;
        }
    }
}
