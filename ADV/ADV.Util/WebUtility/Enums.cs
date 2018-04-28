using System;

namespace ADV.Util.WebUtility
{
    //合格投资人认证
    public enum EnumUserAuthActive
    { 
        未认证 = 0,
        认证中 = 1,
        认证成功 = 2,
        认证失败 = 3
    }

    //文章、公告信息状态
    public enum EnumInfoContent
    { 
        草稿 = 0,
        已发布 = 1,
        已删除 = 2
    }

    //产品状态
    public enum EnumProductStatus
    { 
        项目保存 = 10,
        项目预售 = 20,
        开始募集 = 30,
        募集失败 = 40,
        募集完成 = 50,
        募集成功 = 60
    }
}
