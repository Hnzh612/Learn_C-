using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFframework.Entity
{
    class MyData
    {
        private string colorName = "green";
        private string width = "300";
        private string height = "20";

        /// <summary>
        /// 这样 clolorName 的值就为 red
        /// </summary>
        public string ColorName { get => colorName; set => colorName = value; }
        public string Width { get => width; set => width = value; }
        public string Height { get => height; set => height = value; }
        public string Title { get;set; }
    }
}
