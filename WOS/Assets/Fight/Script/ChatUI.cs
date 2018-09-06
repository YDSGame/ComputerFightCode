using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using Photon;


public class ChatUI : Photon.MonoBehaviour
{
    public readonly List<string> Senders = new List<string>();
    public readonly List<object> Messages = new List<object>();

    public int MessageLimit;

    public bool IsPrivate { get; internal protected set; }

    public int MessageCount
    {
        get
        {
            return this.Messages.Count;
        }
    }
    public void Add(string Sender,object message)
    {
        this.Senders.Add(Sender);
        this.Messages.Add(message);
        this.TruncateMessages();

    }
    public void Add(string[] Senders,object[] messages)
    {
        this.Senders.AddRange(Senders);
        this.Messages.AddRange(messages);
        this.TruncateMessages();
    }
       
    public void TruncateMessages()
    {
        if (this.MessageLimit <= 0 || this.Messages.Count <= this.MessageLimit)
        {
            return;
        }
        int excessCount = this.Messages.Count - this.MessageLimit;
        this.Senders.RemoveRange(0, excessCount);
        this.Messages.RemoveRange(0, excessCount);
    }
    public string ToStringMessages()
    {
        StringBuilder txt = new StringBuilder();
        for (int i = 0; i < this.Messages.Count; i++)
        {
            txt.AppendLine(string.Format("{0}: {1}", this.Senders[i], this.Messages[i]));
        }
        return txt.ToString();
    }
}
