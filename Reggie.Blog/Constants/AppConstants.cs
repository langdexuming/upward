using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Blog.Constants
{
    public class AppConstants
    {

        #region 应用独有

        /// <summary>
        /// 随笔概要文本最大长度
        /// </summary>
        public const int EaasySummaryTextMaxLength = 200;

        /// <summary>
        /// 默认加载博客的数量
        /// </summary>
        public const int LoadInformalEssayCount = 10;

        #endregion

        #region 数据库相关
        /// <summary>
        /// 座右铭
        /// </summary>
        public const string Motto = "Motto";

        /// <summary>
        /// 个人简介
        /// </summary>
        public const string PersonalProfile = "PersonalProfile";

        /// <summary>
        /// 是否展示个人技能
        /// </summary>
        public const string IsShowSkills = "IsShowSkills";

        /// <summary>
        /// 是否展示个人经历
        /// </summary>
        public const string IsShowJobExperiences = "IsShowJobExperiences";

        /// <summary>
        /// 是否展示作品集
        /// </summary>
        public const string IsShowSamples = "IsShowSamples";

        /// <summary>
        /// 是否显示教育信息
        /// </summary>
        public const string IsShowEducationInformation = "IsShowEducationInformation";
        #endregion
    }
}
