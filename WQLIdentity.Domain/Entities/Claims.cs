using System;
using System.Collections.Generic;
using System.Text;

namespace WQLIdentity.Domain.Entities
{
    public class Claims: Entity
    {
        /// <summary>
        /// 声明类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 声明值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
