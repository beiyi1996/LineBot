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
        const string AdminUserId = "U275c68b802e11bb599413ef87dcea051";
        const string LuisAppId = "c2c2930c-4e92-494e-b569-0d5400cd4686";
        const string LuisAppKey = "8c5db4d4a17b4d25a99ee6502ada350f";
        const string Luisdomain = "westus"; //ex.westus

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
                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(ChannelAccessToken);
                string Lineid = ReceivedMessage.events.FirstOrDefault().source.userId;
                var Userinfo = bot.GetUserInfo(Lineid);
                //配合Line verify 回傳一個訊息給後台表示webhook成功連接
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                //回覆訊息
                //this.PushMessage(LineEvent.source.userId,"6666"); //如果想要私訊某個人(userid)的話
                if (LineEvent.type == "message")
                {
                    //if (LineEvent.message.type == "text") //收到文字
                    //    this.ReplyMessage(LineEvent.replyToken, Userinfo.displayName + "你好，我可以回覆任何問題"); //replyToken傳訊息給整個聊天室
                    if (LineEvent.message.type == "sticker") //收到貼圖
                        this.ReplyMessage(LineEvent.replyToken, 1, 2);
                    if (LineEvent.message.type == "location") //GPS定位
                        this.ReplyMessage(LineEvent.replyToken, $"你的位置在 \n {LineEvent.message.latitude},{LineEvent.message.longitude}");
                    if (LineEvent.message.type == "image") //收到圖片
                    {
                        var bytes = this.GetUserUploadedContent(LineEvent.message.id);
                        var guid = Guid.NewGuid().ToString();
                        var filename = $"{guid}.png";
                        var path = System.Web.Hosting.HostingEnvironment.MapPath("~/temp/");
                        System.IO.File.WriteAllBytes(path + filename, bytes);
                        var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                        var url = $"{baseUrl}/temp/{filename}";
                        this.ReplyMessage(LineEvent.replyToken, $"你的圖片位於 \n {url}");
                    }
                    var repmsg = "";
                    if (LineEvent.message.type == "text") //收到文字
                    {
                        //建立LuisClient
                        Microsoft.Cognitive.LUIS.LuisClient lc =
                          new Microsoft.Cognitive.LUIS.LuisClient(LuisAppId, LuisAppKey, true, Luisdomain);

                        //Call Luis API 查詢
                        var ret = lc.Predict(LineEvent.message.text).Result;
                        if (ret.Intents.Count() <= 0)
                            repmsg = $"你說了 '{LineEvent.message.text}' ，但我看不懂喔!";
                        else if (ret.TopScoringIntent.Name == "None")
                            repmsg = $"你說了 '{LineEvent.message.text}' ，但不在我的服務範圍內喔!";
                        else
                        {
                            repmsg = $"OK，你想 '{ret.TopScoringIntent.Name}'，";
                            if (ret.Entities.Count > 0)
                            {
                                repmsg += $"想要的是 '{ ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value }' 和 " +
                                $"'{ret.Entities.ElementAtOrDefault(1).Value.FirstOrDefault().Value}' 和 " +
                                $"'{ret.Entities.LastOrDefault().Value.FirstOrDefault().Value }' ";
                            }
                        }
                        //回覆
                        this.ReplyMessage(LineEvent.replyToken, repmsg);
                    }
                }

                if (LineEvent.type == "postback")
                {
                    var data = LineEvent.postback.data;
                    var datetime = LineEvent.postback.Params.datetime;
                    this.ReplyMessage(LineEvent.replyToken, $"觸發了postback \n 資料為: {data} \n 選擇結果: {datetime}");
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
