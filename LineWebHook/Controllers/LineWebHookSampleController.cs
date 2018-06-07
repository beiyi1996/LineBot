using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LineWebHook.Controllers
{
    public class LineBotWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        const string channelAccessToken = "9YwfPyWbdWkYqcs5aistrwDTZaPwLAivy+9vpvKS034TVyF9Cj7UhHcttzo4CJ1+zLH7YadJ7B5U9a9ho/4Kg6mU+Z5u0bHvo8zo7y3+8BwccBpL+4QDGrknX16T3roNmLnxVaOhmwkyXXQ/G2INFwdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId= "U275c68b802e11bb599413ef87dcea051";

        [Route("api/LineWebHookSample")]
        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                //設定ChannelAccessToken(或抓取Web.Config)
                this.ChannelAccessToken = channelAccessToken;
                //取得Line Event(範例，只取第一個)
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 回傳一個訊息給後台表示webhook成功連接
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                //回覆訊息
                //this.PushMessage(LineEvent.source.userId,"6666"); //如果想要私訊某個人(userid)的話
                if (LineEvent.type == "message")
                {
                    if (LineEvent.message.type == "text") //收到文字
                        this.ReplyMessage(LineEvent.replyToken, "我可以回復任何問題"); //replyToken傳訊息給整個聊天室
                    if (LineEvent.message.type == "sticker") //收到貼圖
                        this.ReplyMessage(LineEvent.replyToken, 1, 2);
                }
                //response OK
                return Ok(); //回傳給line server
            }
            catch (Exception ex)
            {
                //如果發生錯誤，傳訊息給Admin
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}
