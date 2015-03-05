using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace EventLst.Core
{
    public class ResultsDirector
    {
        private IBuilder _builder;

        public ResultsDirector(IBuilder injectedBuilder)
        {
            _builder = injectedBuilder;
        }

        public void Construct()
        {

            _builder.Load();

            _builder.Map();

        }
    }
}