using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Interface
{
    /// <summary>
    /// 在执行任何模块、流程、步骤时,应当：
    ///                                 1.初始化操作(SetUp)：
    ///                                             1. check value(attribute)
    ///                                             2.Log (Read M/Write 1)(attribute)
    ///                                             3.init some hardware.
    ///                                 2.收尾工作(TearDown)：
    ///                                                     1.expction collection
    ///                                                     2.Validation Infomation（weak event）
    /// </summary>
    internal interface IProcedureRule
    {
        Task<bool> CheckData();
        Task<bool> SetUp();
        Task<bool> Execute();
        Task<bool> TearDown();
    }
}
