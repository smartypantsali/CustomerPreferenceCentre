using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportGenerationService.Utilities
{
    public interface IFactory<T>
    {
        T CreateInstance();
    }
}
