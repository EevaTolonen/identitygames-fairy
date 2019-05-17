using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogText
{
    public int Id;
    public string Text;
    public List<Response> responses;

    public struct Response
    {
        public int Id;
        public string Text;

        public Response(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }

    public DialogText()
    {
    }

    public DialogText(int id, string text, List<Response> responses)
    {
        this.Id = id;
        this.Text = text;
        this.responses = responses;
    }
}
