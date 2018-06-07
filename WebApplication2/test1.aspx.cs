using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            isRock.LineBot.Bot bot = new isRock.LineBot.Bot("ffn5jJSAOr/bfLkR5pkhPpzohKkMtgJ12QrEgYUkzAdcFhYP12cut0xWUgFHSZX5zLH7YadJ7B5U9a9ho/4Kg6mU+Z5u0bHvo8zo7y3+8BxQxYz2Rq4ye/VbPpun6dN1bnnTKfXerTPfYFiL3ZO2ewdB04t89/1O/w1cDnyilFU=");
            bot.PushMessage("U275c68b802e11bb599413ef87dcea051", "哈囉! 我是Tube");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            isRock.LineBot.Bot bot = new isRock.LineBot.Bot("ffn5jJSAOr/bfLkR5pkhPpzohKkMtgJ12QrEgYUkzAdcFhYP12cut0xWUgFHSZX5zLH7YadJ7B5U9a9ho/4Kg6mU+Z5u0bHvo8zo7y3+8BxQxYz2Rq4ye/VbPpun6dN1bnnTKfXerTPfYFiL3ZO2ewdB04t89/1O/w1cDnyilFU=");
            bot.PushMessage("U275c68b802e11bb599413ef87dcea051",2,176);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            isRock.LineBot.Bot bot = new isRock.LineBot.Bot("ffn5jJSAOr/bfLkR5pkhPpzohKkMtgJ12QrEgYUkzAdcFhYP12cut0xWUgFHSZX5zLH7YadJ7B5U9a9ho/4Kg6mU+Z5u0bHvo8zo7y3+8BxQxYz2Rq4ye/VbPpun6dN1bnnTKfXerTPfYFiL3ZO2ewdB04t89/1O/w1cDnyilFU=");
            bot.PushMessage("U275c68b802e11bb599413ef87dcea051",new Uri("https://www.90daykorean.com/wp-content/uploads/2015/04/Tube.png"));
        }
    }
}