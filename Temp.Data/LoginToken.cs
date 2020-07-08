using System;
using System.Collections.Generic;
using System.Text;

namespace Temp.Data
{
    public class LoginToken
    {
        public int KID { get; set; }
        public DateTime CreateTime { get; set; }
        //public DateTime UpdateTime { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token有效期到期时间
        /// </summary>

        public string TokenExpiration { get; set; }

        /// <summary>
        /// 登录者id
        /// </summary>

        public string LoginUserId { get; set; }

        /// <summary>
        /// 登录人的账号
        /// </summary>

        public string LoginUserAccount { get; set; }

        /// <summary>
        /// 登录账户类型
        /// </summary>
        public int LoginUserType { get; set; }

        /// <summary>
        /// 登录设备ip
        /// </summary>
        public string IpAddr { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>

        public int PlatForm { get; set; }

        /// <summary>
        /// 是否退出登录0未退，1退出
        /// </summary>

        public int IsLogOut { get; set; }

        /// <summary>
        /// 登录结果
        /// </summary>

        public string LoginResult { get; set; }
    }
}
