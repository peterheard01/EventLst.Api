using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLst.Core
{
    public interface IBuilder
    {
        void Load();

        void Map();
    }

    public interface IBuilder<TInputModel, TOutputModel> : IBuilder
    {
        TInputModel InputModel { get; set; }

        TOutputModel OutputModel { get; set; }
    }


}
