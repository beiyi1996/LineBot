using isRock.LineBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LineWebHook
{
    public partial class _default : System.Web.UI.Page
    {
        const string channelAccessToken = "9YwfPyWbdWkYqcs5aistrwDTZaPwLAivy+9vpvKS034TVyF9Cj7UhHcttzo4CJ1+zLH7YadJ7B5U9a9ho/4Kg6mU+Z5u0bHvo8zo7y3+8BwccBpL+4QDGrknX16T3roNmLnxVaOhmwkyXXQ/G2INFwdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "U275c68b802e11bb599413ef87dcea051";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var bot = new Bot(channelAccessToken);
            bot.PushMessage(AdminUserId, $"測試 {DateTime.Now.ToString()} ! ");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var bot = new Bot(channelAccessToken);
            bot.PushMessage(AdminUserId, 1, 2);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var bot = new Bot(channelAccessToken);
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            actions.Add(new isRock.LineBot.MessageAction() { label = "標題-文字回覆", text = "回覆文字" });
            actions.Add(new isRock.LineBot.DateTimePickerAction() { label = "請選擇時間", mode = "datetime" });
            actions.Add(new isRock.LineBot.UriAction() { label = "標題-開啟URL", uri = new Uri("https://www.youtube.com/?gl=TW&hl=zh-tw") });
            actions.Add(new isRock.LineBot.PostbackAction() { label = "標題-發生postback", data = "abc=aaa&def=111" });

            var BottonTempalteMsg = new isRock.LineBot.ButtonsTemplate()
            {
                title = "選項",
                text = "請選擇一個",
                altText = "請在手機上檢視",
                thumbnailImageUrl = new Uri("https://voniblog.com/kakao-friends/"),
                actions = actions
            };
            bot.PushMessage(AdminUserId, BottonTempalteMsg);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            var bot = new Bot(channelAccessToken);
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            actions.Add(new isRock.LineBot.MessageAction() { label = "Yes", text = "Yes" });
            actions.Add(new isRock.LineBot.MessageAction() { label = "NO", text = "No" });

            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                text = "請選擇一個",
                altText = "請在手機上檢視",
                actions = actions
            };
            bot.PushMessage(AdminUserId, ConfirmTemplateMsg);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            isRock.LineBot.Bot bot = new isRock.LineBot.Bot("9YwfPyWbdWkYqcs5aistrwDTZaPwLAivy+9vpvKS034TVyF9Cj7UhHcttzo4CJ1+zLH7YadJ7B5U9a9ho/4Kg6mU+Z5u0bHvo8zo7y3+8BwccBpL+4QDGrknX16T3roNmLnxVaOhmwkyXXQ/G2INFwdB04t89/1O/w1cDnyilFU=");
            bot.PushMessage("U275c68b802e11bb599413ef87dcea051", "hihihihihihi~");
            var Userinfo = bot.GetUserInfo("U275c68b802e11bb599413ef87dcea051");
            Response.Write(Userinfo.displayName + Userinfo.pictureUrl + "<br/>" + Userinfo.statusMessage);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            isRock.LineBot.Bot bot = new isRock.LineBot.Bot("9YwfPyWbdWkYqcs5aistrwDTZaPwLAivy+9vpvKS034TVyF9Cj7UhHcttzo4CJ1+zLH7YadJ7B5U9a9ho/4Kg6mU+Z5u0bHvo8zo7y3+8BwccBpL+4QDGrknX16T3roNmLnxVaOhmwkyXXQ/G2INFwdB04t89/1O/w1cDnyilFU=");
            var Userinfo = bot.GetUserInfo("U275c68b802e11bb599413ef87dcea051");
            Response.Write(Userinfo.displayName + "<br/>" + Userinfo.pictureUrl + "<br/>" + Userinfo.statusMessage);
        }
    }
}