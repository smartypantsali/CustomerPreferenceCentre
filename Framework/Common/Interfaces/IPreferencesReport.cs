using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Common.Interfaces
{
    public interface IReport<T1, T2>
    {
        Dictionary<string, Stack<string>> Report { get; }
        T1 GenerateReport(T2 arg);
    }
}
