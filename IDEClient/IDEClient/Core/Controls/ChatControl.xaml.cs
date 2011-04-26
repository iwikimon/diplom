using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IDEClient.Core.Controls
{
    public partial class ChatControl : UserControl
    {
        private string nick = "messko";
        public ChatControl()
        {
            InitializeComponent();
        }

        private void msg_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Paragraph p = new Paragraph();
                p.Inlines.Add(DateSpan(DateTime.Now));
                p.Inlines.Add(LoginSpan(nick));
                p.Inlines.Add(new LineBreak());
                p.Inlines.Add(MsgSpan(msg.Text));
                chatRtb.Blocks.Add(p);
                msg.Text = "";
            }
        }

        private Span DateSpan(DateTime date)
        {
            Span span = new Span();
            span.FontStyle = FontStyles.Normal;
            span.FontSize = 10;
            span.FontWeight = FontWeights.Bold;
            span.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            span.Inlines.Add("[" + date + "] ");
            return span;
        }

        private Span LoginSpan(string login)
        {
            Span span = new Span();
            span.FontStyle = FontStyles.Italic;
            span.FontSize = 12;
            span.FontWeight = FontWeights.Bold;
            span.Foreground = new SolidColorBrush(Color.FromArgb(255, 155, 155, 155));
            span.Inlines.Add(login+":");
            return span;
        }

        private Span MsgSpan(string message)
        {
            Span span = new Span();
            span.FontStyle = FontStyles.Normal;
            span.FontSize = 10;
            span.FontWeight = FontWeights.Bold;
            span.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            span.Inlines.Add(message);
            return span;
        }
    }
}
